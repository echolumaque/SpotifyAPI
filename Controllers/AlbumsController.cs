using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Spotify.Models;

namespace Spotify.Controllers
{
    [RoutePrefix("albums")]
    public class AlbumsController : ApiController
    {
        public IHttpActionResult GetAllAlbums()
        {
            try
            {
                using (var albums = new AlbumsEntities())
                {
                    var listOfAlbums = albums.Albums.DistinctBy(x => x.AlbumName).Select(x => new 
                    {
                        x.AlbumName,
                        x.Artist,
                        x.Images,
                        x.Year
                    }).ToList();

                    return Ok(listOfAlbums);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult GetSongByAlbumName(string albumName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(albumName.ToString()))
                {
                    return BadRequest("Parameter albumName is required1");
                }
                else
                {
                    using (var albums = new AlbumsEntities())
                    {
                        if (albums.Albums.Any(x => x.AlbumName.Equals(albumName)))
                        {
                            return Ok(albums.Albums.ToList().Where(x => x.AlbumName.Equals(albumName)));
                        }
                        else
                        {
                            return Content(HttpStatusCode.NotFound, "Album not found!");
                        }
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