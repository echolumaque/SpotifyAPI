using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Spotify.Models;

namespace Spotify.Controllers
{
    [RoutePrefix("users/likes")]
    public class UsersLikesController : ApiController
    {
        [HttpGet]
        [Route("{userId:int}")]
        public HttpResponseMessage GetLikes([FromUri] int userId)
        {
            try
            {
                using (var likes = new UsersLikesEntities())
                {
                    return likes.QueryUsersLikes(userId).ToList().Count == 0 ? 
                        Request.CreateResponse(System.Net.HttpStatusCode.NotFound, new NullReferenceException("The requested data is empty")) :
                        Request.CreateResponse(System.Net.HttpStatusCode.OK, likes.QueryUsersLikes(userId).ToList()); 
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, ex);
            }
        }
    }
}