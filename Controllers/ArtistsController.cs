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
        [Route("ArtistTopSongs")]
        public IHttpActionResult GetArtistTopSongs(int userId, string artistName)
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

        [HttpGet]
        [Route("artistinfo")]
        public IHttpActionResult GetArtistInfo(int userId, string artistName)
        {
            try
            {
                using (var artist = new ArtistEntities())
                {
                    return UserExist(userId) ? Ok(artist.Artists.Where(x => x.Artist1 == artistName).Select(x => new
                    {
                        x.Artist1,
                        x.Followers,
                        x.Image
                    }).FirstOrDefault()) :
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