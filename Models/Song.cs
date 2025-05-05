using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace CSCI3110_TermProject.Models;

public class Song
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Genre { get; set; }
    public string? FilePath { get; set; }

    [ValidateNever]
    public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
}
