using System;
using System.Linq;
using System.Web.Http;
using Spotify.Helpers;
using Spotify.Models;

namespace Spotify.Controllers
{
    [RoutePrefix("Artists")]
    public class ArtistsController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult GetArtist(int userId, string artistName)
        {
            try
            {
                using (var artist = new ArtistEntities())
                {
                    return UserExist(userId) ? Ok(artist.QueryArtistTopSongs(artistName).ToList()) :
                        Content(System.Net.HttpStatusCode.NotFound, new { Message = $"User with user id: {userId} is not found on the database" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}