using System.Linq;
using System.Web.Http;
using Spotify.Models;

namespace Spotify.Helpers
{
    public class BaseApiController : ApiController
    {
        public static bool UserExist(int userId)
        {
            using (var users = new UsersEntities())
            {
                return users.Users.AsNoTracking().Any(x => x.UserID == userId);
            }
        }
    }
}