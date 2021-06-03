using Newtonsoft.Json;

namespace Spotify.Models
{
    public class AddNewPlaylistSongModel
    {
        [JsonProperty("SongID")]
        public int SongID { get; set; }

        [JsonProperty("UserID")]
        public int UserID { get; set; }
    }
}