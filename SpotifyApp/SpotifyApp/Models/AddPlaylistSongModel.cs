using Newtonsoft.Json;

namespace SpotifyApp.Models
{
    public class AddPlaylistSongModel
    {
        [JsonProperty("SongID")]
        public int SongID { get; set; }

        [JsonProperty("UserID")]
        public int UserID { get; set; }

        [JsonProperty("PlaylistID")]
        public int PlaylistID { get; set; }
    }
}