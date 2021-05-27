using Newtonsoft.Json;

namespace SpotifyApp.Models
{
    public class UserLikeSongsModel
    {
        [JsonProperty("UserID")]
        public int UserID { get; set; }
        [JsonProperty("SongID")]
        public int SongID { get; set; }
    }
}
