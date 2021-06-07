using Newtonsoft.Json;

namespace SpotifyApp.Models
{
    public class ArtistTopSongsModel
    {
        [JsonProperty("Artist")]
        public string Artist { get; set; }

        [JsonProperty("Image")]
        public string Image { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Duration")]
        public int Duration { get; set; }
    }

}
