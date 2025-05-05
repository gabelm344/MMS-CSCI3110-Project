using CSCI3110_TermProject.Models;

public interface IPlaylistRepository
{
    Task<IEnumerable<Playlist>> GetAllAsync();
    Task<Playlist?> GetByIdAsync(int id);
    Task AddAsync(Playlist playlist);
    Task DeleteAsync(int id);
    Task UpdateAsync(Playlist playlist);
    Task SaveAsync();
    Task AddSongToPlaylistAsync(int playlistId, int songId);
    Task RemoveSongFromPlaylistAsync(int playlistId, int songId);
}
