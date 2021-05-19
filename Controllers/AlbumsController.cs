using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Spotify.Helpers;
using Spotify.Models;

namespace Spotify.Controllers
{
    [RoutePrefix("albums")]
    public class AlbumsController : ApiController
    {
        public HttpResponseMessage GetAlbums()
        {
            try
            {
                using (var albums = new AlbumsEntities())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, albums.Albums.ToList());
                }
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}