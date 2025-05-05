using CSCI3110_TermProject.Models;
using CSCI3110_TermProject.Services;
using Microsoft.EntityFrameworkCore;

public class SongRepository : ISongRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SongRepository> _logger;

    public SongRepository(ApplicationDbContext context, ILogger<SongRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<IEnumerable<Song>> GetAllAsync()
        => await _context.Songs.ToListAsync();

    public async Task<Song?> GetByIdAsync(int id)
        => await _context.Songs.FindAsync(id);

    public async Task AddAsync(Song song)
    {
        _logger.LogInformation("Repository: Adding song {Title}", song.Title);
        _context.Songs.Add(song);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Repository: SaveChangesAsync completed");
    }


    public async Task DeleteAsync(int id)
    {
        var s = await _context.Songs.FindAsync(id);
        if (s != null) { _context.Songs.Remove(s); await _context.SaveChangesAsync(); }
    }
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Song song)
    {
        _context.Songs.Update(song);
        await _context.SaveChangesAsync();
    }
}
