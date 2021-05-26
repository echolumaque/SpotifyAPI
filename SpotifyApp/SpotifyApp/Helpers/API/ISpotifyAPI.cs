using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using SpotifyApp.Models;

namespace SpotifyApp.Helpers.API
{
    public interface ISpotifyAPI
    {
        [Get("/albums")]
        Task<IEnumerable<AlbumsModel>> GetAlbums();

        [Get("/albums?albumName={albumName}")]
        Task<IEnumerable<AlbumsModel>> GetAlbumSongs(string albumName);

    }
}
