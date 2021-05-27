using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Spotify.Models;

namespace Spotify.Controllers
{
    [RoutePrefix("userslikes")]
    public class UsersLikesController : ApiController
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
                        return users.Users.Any(x => x.UserID == userId) ? Ok(likes.QueryUsersLikes(userId).ToList())
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
                using (var likes = new UsersLikesEntities())
                {
                    likes.UsersLikeSongs.Add(usersLikeSong);

                    await likes.SaveChangesAsync();
                    return Ok(new { Message = $"Succesfully liked song {usersLikeSong.UserID} by user {usersLikeSong.UserID}" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}