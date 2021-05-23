using System;
using System.Linq;
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
                        : Content(System.Net.HttpStatusCode.NotFound, "");
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