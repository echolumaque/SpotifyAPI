using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Spotify.Models;

namespace Spotify.Helpers
{
    public class EncryptAttribute : AuthorizationFilterAttribute
    {
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext.Request.Headers.Authorization.Parameter == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                string[] decodedCredentials = decodedAuthToken.Split(':');
                string username = decodedCredentials[0];
                string password = decodedCredentials[1];

                if (await CheckIfUserExists(username, password))
                {

                }
                else
                    actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);

            }
        }

        private async Task<bool> CheckIfUserExists(string username, string password)
        {
            using (var users = new UsersEntities())
            {
                var hashedPassword = await Argon2iEncryption.HashPassword(password);
                return users.Users.Any(x => x.Username.Equals(username) && x.PasswordHash.Equals(hashedPassword));
            }
        }
    }
}