using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Spotify.Helpers;
using Spotify.Models;

namespace Spotify.Controllers
{
    [RoutePrefix("usershiddensongs")]
    public class UsersHiddenSongsController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult GetUsersHiddenSongs(int userId)
        {
            try
            {
                using (var hiddenSongs = new UsersHiddenSongsEntities())
                {
                    return UserExist(userId) ? Ok(hiddenSongs.QueryUsersHiddenSongs(userId).ToList()) :
                        Content(System.Net.HttpStatusCode.NotFound, new { Message = $"User with user id: {userId} is not found on the database" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("hide")]
        [HttpPost]
        public async Task<IHttpActionResult> AddHiddenSongs([FromBody] UsersHiddenSong usersHiddenSong)
        {
            try
            {
                using (var hiddenSong = new UsersHiddenSongsEntities())
                {
                   
                    if (UserExist(usersHiddenSong.UserID))
                    {
                        hiddenSong.UsersHiddenSongs.Add(usersHiddenSong);
                        await hiddenSong.SaveChangesAsync();

                        return Ok(new { Message = $"Song with id: {usersHiddenSong.SongID} has been hidden hidden by: {usersHiddenSong.UserID}" });
                    }
                    else
                    {
                        return Content(System.Net.HttpStatusCode.NotFound, new { Message = $"User with user id: {usersHiddenSong.UserID} is not found on the database" });
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
