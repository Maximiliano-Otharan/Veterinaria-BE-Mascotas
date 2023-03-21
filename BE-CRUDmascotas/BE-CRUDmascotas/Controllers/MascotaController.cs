using BE_CRUDmascotas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using BE_CRUDmascotas.Models.DTO;
using BE_CRUDmascotas.Models.Repository;

namespace BE_CRUDmascotas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMascotaRepository _mascotaRepository;

        public MascotaController(IMapper mapper, IMascotaRepository mascotaRepository)
        {
            _mapper = mapper;
            _mascotaRepository = mascotaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                Thread.Sleep(2000);
                var listMascotas = await _mascotaRepository.GetListMascotas();

                var listMascotaDto = _mapper.Map<IEnumerable<MascotaDTO>>(listMascotas);

                return Ok(listMascotaDto);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var mascota = await _mascotaRepository.GetMascota(id);
                if(mascota == null)
                {
                    return NotFound();
                }

                var mascotaDto = _mapper.Map<MascotaDTO>(mascota);

                return Ok(mascotaDto);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var mascota = await _mascotaRepository.GetMascota(id);

                if(mascota == null)
                {
                    return NotFound();
                }

                await _mascotaRepository.DeleteMascota(mascota);

                return NoContent();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(MascotaDTO mascotaDto)
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDto);

                mascota.FechaCreacion = DateTime.Now;

                mascota = await _mascotaRepository.AddMascota(mascota);

                mascotaDto = _mapper.Map<MascotaDTO>(mascota);
                return CreatedAtAction("Get", new { id = mascotaDto.Id }, mascotaDto);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MascotaDTO mascotaDto)
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDto);

                if (id != mascota.Id)
                {
                    return BadRequest();
                }
                var mascotaItem = await _mascotaRepository.GetMascota(id);
                if(mascotaItem == null)
                {
                    return NotFound();
                }
                
                await _mascotaRepository.UpdateMascota(mascota);

                return NoContent();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
