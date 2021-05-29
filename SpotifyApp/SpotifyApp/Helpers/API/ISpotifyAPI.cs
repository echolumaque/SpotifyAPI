using System.Collections.Generic;
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

        [Post("/userslikes/addlike")]
        Task LikeASong([Body] UserLikeSongsModel userLikeSongsModel);

        [Get("/userslikes?userId={userId}")]
        Task<IEnumerable<AlbumsModel>> GetUserLikedSongs(int userId);

        [Get("/usershiddensongs?userId={userId}")]
        Task<IEnumerable<int>> GetUsersHiddenSongs(int userId);
    }
}