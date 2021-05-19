using System;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Spotify.Helpers;
using Spotify.Models;

namespace Spotify.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        [HttpPut]
        [Route("changepassword")]
        public async Task<HttpResponseMessage> ChangePasword([FromBody] User user)
        {
            try
            {
                using (var users = new UsersEntities())
                {
                    var userDataToBeChanged = await users.Users.FirstAsync(x => x.UserID == user.UserID);
                    userDataToBeChanged.Username = user.Username;
                    await users.SaveChangesAsync();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}