using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spotify.Helpers;
using Spotify.Models;

namespace Spotify.Controllers
{
    [RoutePrefix("UsersPlaylist")]
    public class UsersPlaylistController : BaseApiController
    {
        [HttpGet] //song for each playlist
        [Route("playlistsongs")]
        public IHttpActionResult GetUserPlaylistSong(int userId, int playlistId)
        {
            try
            {
                using (var playlist = new UserPlaylistEntities())
                {
                    return UserExist(userId) ? Ok(playlist.QueryUserPlaylist(userId, playlistId).ToList()) :
                        Content(System.Net.HttpStatusCode.NotFound, new { Message = $"User with user id: {userId} is not found on the database" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("playlistname")]
        public IHttpActionResult GetUserPlaylists(int userId)
        {
            try
            {
                using (var playlist = new UserPlaylistEntities())
                {
                    return UserExist(userId) ? Ok(playlist.QueryUserPlaylistName(userId).ToList()) :
                        Content(System.Net.HttpStatusCode.NotFound, new { Message = $"User with user id: {userId} is not found on the database" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addsong")]
        public async Task<IHttpActionResult> AddSongToPlaylist([FromBody] PlaylistSong playlistSong)
        {
            try
            {
                using (var playlist = new UserPlaylistEntities())
                {
                    playlist.PlaylistSongs.Add(playlistSong);
                    await playlist.SaveChangesAsync();
                    return Ok(new { Message = $"Succesfully added to playlist by user {playlistSong.UserID}" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addplaylist")] //adds new playlist then adds the song frm the enwly created playlist.
        public async Task<IHttpActionResult> AddPlaylist([FromBody] ArrayList arayList)
        {
            try
            {
                var newPlaylist = JsonConvert.DeserializeObject<Playlist>(arayList[0].ToString());
                var playlistSong = JsonConvert.DeserializeObject<AddNewPlaylistSongModel>(arayList[1].ToString());

                using (var playlist = new UserPlaylistEntities())
                {
                    playlist.Playlists.Add(newPlaylist);
                    await playlist.SaveChangesAsync();

                    var playlistSongs = new PlaylistSong
                    {
                        PlaylistID = playlist.Playlists.Where(x => x.UserID == playlistSong.UserID).OrderByDescending(x => x.PlaylistID).FirstOrDefault().PlaylistID,
                        SongID = playlistSong.SongID,
                        UserID = playlistSong.UserID
                    };

                    playlist.PlaylistSongs.Add(playlistSongs);
                    await playlist.SaveChangesAsync();

                    return Ok(new { Message = $"Succesfully added to playlist by user {playlistSong.UserID}" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}