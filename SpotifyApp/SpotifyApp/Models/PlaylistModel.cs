using System.Collections.Generic;
using Newtonsoft.Json;

//get playlist by user from db
namespace SpotifyApp.Models
{
    public class PlaylistModel
    {
        public Playlist Playlist { get; set; }
    }

    public class Playlist
    {
        [JsonProperty("PlaylistName")]
        public string PlaylistName { get; set; }

        [JsonProperty("PlaylistImage")]
        public string PlaylistImage { get; set; }

        [JsonProperty("PlaylistID")]
        public int PlaylistID { get; set; }

        [JsonProperty("Images")]
        public string Images { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Artist")]
        public string Artist { get; set; }

        [JsonProperty("Duration")]
        public int Duration { get; set; }
    }
}