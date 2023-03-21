namespace BE_CRUDmascotas.Models.Repository
{
    public interface IMascotaRepository
    {
        Task<List<Mascota>> GetListMascotas();
        Task<Mascota> GetMascota(int id);
        Task DeleteMascota(Mascota mascota);
        Task<Mascota> AddMascota(Mascota mascota);
        Task UpdateMascota(Mascota mascota);
    }
}
