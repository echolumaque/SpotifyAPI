using Newtonsoft.Json;

namespace SpotifyApp.Models
{
    public class AddNewPlaylistModel
    {
        [JsonProperty("PlaylistName")]
        public string PlaylistName { get; set; }

        [JsonProperty("PlaylistImage")]
        public string PlaylistImage { get; set; }

        [JsonProperty("UserID")]
        public int UserID { get; set; }
    }
}
