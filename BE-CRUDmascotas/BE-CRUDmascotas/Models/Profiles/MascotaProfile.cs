using AutoMapper;
using BE_CRUDmascotas.Models.DTO;

namespace BE_CRUDmascotas.Models.Profiles
{
    public class MascotaProfile: Profile
    {
        public MascotaProfile() 
        {
            CreateMap<Mascota, MascotaDTO>();
            CreateMap<MascotaDTO, Mascota>();
        }
    }
}
