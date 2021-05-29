using System;
using Newtonsoft.Json;
using Prism.Commands;

namespace SpotifyApp.Models
{
    public class AlbumsModel
    {
        [JsonProperty("SongID")]
        public int SongId { get; set; }

        [JsonProperty("Artist")]
        public string Artist { get; set; }

        [JsonProperty("Images")]
        public string Images { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Duration")]
        public int Duration { get; set; }

        [JsonProperty("IsExplicit")]
        public bool IsExplicit { get; set; }

        [JsonProperty("AlbumName")]
        public string AlbumName { get; set; }
        [JsonProperty("Year")]
        public string Year { get; set; }

        public DelegateCommand<AlbumsModel> GotoAlbumSongsCommand { get; set; }
        public DelegateCommand<AlbumsModel> GotoSongCommand { get; set; }
        public DelegateCommand<AlbumsModel> GotoSongInfoCommand { get; set; }
        public double SongOpacity { get; set; }//opacity if song is hidden or not

    }
}
