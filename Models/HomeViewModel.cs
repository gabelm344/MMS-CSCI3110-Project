namespace CSCI3110_TermProject.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Song> Songs { get; set; } = new List<Song>();
        public IEnumerable<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
