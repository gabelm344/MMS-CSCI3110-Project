using CSCI3110_TermProject.Models;
using Microsoft.EntityFrameworkCore;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly ApplicationDbContext _context;

    public PlaylistRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Playlist>> GetAllAsync()
    {
        return await _context.Playlists
            .Include(p => p.PlaylistSongs)
            .ThenInclude(ps => ps.Song)
            .ToListAsync();
    }

    public async Task<Playlist?> GetByIdAsync(int id)
    {
        return await _context.Playlists
            .Include(p => p.PlaylistSongs)
            .ThenInclude(ps => ps.Song)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Playlist playlist)
    {
        _context.Playlists.Add(playlist);
        await SaveAsync();
    }
    public async Task AddSongToPlaylistAsync(int playlistId, int songId)
    {
        var exists = await _context.PlaylistSongs
            .AnyAsync(ps => ps.PlaylistId == playlistId && ps.SongId == songId);

        if (!exists)
        {
            _context.PlaylistSongs.Add(new PlaylistSong
            {
                PlaylistId = playlistId,
                SongId = songId
            });
            await SaveAsync();
        }
    }

    public async Task RemoveSongFromPlaylistAsync(int playlistId, int songId)
    {
        var entry = await _context.PlaylistSongs
            .FirstOrDefaultAsync(ps => ps.PlaylistId == playlistId && ps.SongId == songId);

        if (entry != null)
        {
            _context.PlaylistSongs.Remove(entry);
            await SaveAsync();
        }
    }
    public async Task UpdateAsync(Playlist playlist)
    {
        _context.Playlists.Update(playlist);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var pl = await _context.Playlists.FindAsync(id);
        if (pl != null)
        {
            _context.Playlists.Remove(pl);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
