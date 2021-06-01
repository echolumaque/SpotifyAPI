using Newtonsoft.Json;
using Prism.Commands;

//get playlist by user from db
namespace SpotifyApp.Models
{
    public class Playlist
    {
        [JsonProperty("Images")]
        public string Images { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Artist")]
        public string Artist { get; set; }

        [JsonProperty("Duration")]
        public int Duration { get; set; }

        [JsonProperty("PlaylistName")]
        public string PlaylistName { get; set; }

        [JsonProperty("PlaylistImage")]
        public string PlaylistImage { get; set; }

        [JsonProperty("PlaylistID")]
        public int PlaylistID { get; set; }

        public DelegateCommand<Playlist> AddSongToPlaylistCommand { get; set; }
    }
}