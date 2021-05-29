using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Spotify.Helpers;
using Spotify.Models;

namespace Spotify.Controllers
{
    [RoutePrefix("userslikes")]
    public class UsersLikesController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult GetLikes(int userId)
        {
            try
            {
                using (var likes = new UsersLikesEntities())
                {
                    using (var users = new UsersEntities())
                    {
                        return UserExist(userId) ? Ok(likes.QueryUsersLikes(userId).ToList())
                        : Content(System.Net.HttpStatusCode.NotFound, new { Message = $"User with user id: {userId} is not found on the database" });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addlike")]
        public async Task<IHttpActionResult> AddSongToUserLikes([FromBody] UsersLikeSong usersLikeSong)
        {
            try
            {
                if (UserExist(usersLikeSong.UserID))
                {
                    using (var likes = new UsersLikesEntities())
                    {
                        likes.UsersLikeSongs.Add(usersLikeSong);

                        await likes.SaveChangesAsync();
                        return Ok(new { Message = $"Succesfully liked song {usersLikeSong.UserID} by user {usersLikeSong.UserID}" });
                    }
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.NotFound, new { Message = $"User with user id: {usersLikeSong.UserID} is not found on the database" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}