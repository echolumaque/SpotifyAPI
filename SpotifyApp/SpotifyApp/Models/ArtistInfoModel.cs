using Newtonsoft.Json;

namespace SpotifyApp.Models
{
    public class ArtistInfoModel
    {
        [JsonProperty("Artist1")]
        public string Artist1 { get; set; }

        [JsonProperty("Followers")]
        public int Followers { get; set; }

        [JsonProperty("Image")]
        public string Image { get; set; }
    }

    
}
