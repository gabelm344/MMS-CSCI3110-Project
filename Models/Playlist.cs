namespace CSCI3110_TermProject.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

public class Playlist
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string UserId { get; set; }

    [ValidateNever]
    public ApplicationUser User { get; set; }

    [ValidateNever]
    public ICollection<PlaylistSong> PlaylistSongs { get; set; }
        = new List<PlaylistSong>();
}
