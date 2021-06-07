using System.Collections;
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

        [Get("/userslikes?userId={userId}")]
        Task<IEnumerable<AlbumsModel>> GetUserLikedSongs(int userId);

        [Post("/userslikes/addlike")]
        Task LikeASong([Body] UserLikeSongsModel userLikeSongsModel);

        [Get("/usershiddensongs?userId={userId}")]
        Task<IEnumerable<int>> GetUsersHiddenSongs(int userId);

        [Post("/usershiddensongs/hide")]
        Task HideASong([Body] UserLikeSongsModel userHideModel);

        [Get("/UsersPlaylist/playlistsongs?userId={userId}")]
        Task<IEnumerable<Playlist>> GetUserPlaylistSongs(int userId);

        [Get("/UsersPlaylist/playlistname?userId={userId}")]
        Task<IEnumerable<Playlist>> GetUserPlaylists(int userId);

        [Post("/UsersPlaylist/addsong")]
        Task AddSongToPlaylist(AddPlaylistSongModel addPlaylistSongModel);

        [Post("/UsersPlaylist/addplaylist")]
        Task AddNewPlaylist(ArrayList arrayList); //adds new playlist then adds the song frm the enwly created playlist.

        [Get("/artists/artistinfo?userId={userId}&artistName={artistName}")]
        Task<ArtistInfoModel> GetArtistInfo(int userId, string artistName);

        [Get("/artists/ArtistTopSongs?userId={userId}&artistName={artistName}")]
        Task<IEnumerable<ArtistTopSongsModel>> GetArtistTopSongs(int userId, string artistName);
    }
}