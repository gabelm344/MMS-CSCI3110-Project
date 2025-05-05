using CSCI3110_TermProject.Models;

namespace CSCI3110_TermProject.Services
{
    public interface ISongRepository
    {
        Task<IEnumerable<Song>> GetAllAsync();
        Task<Song?> GetByIdAsync(int id);
        Task AddAsync(Song song);
        Task DeleteAsync(int id);
        Task UpdateAsync(Song song);
        Task SaveAsync();
    }
}
