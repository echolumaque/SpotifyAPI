using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Spotify.Helpers
{
    public class ForceHTTPSAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);

                var uriBuilder = new UriBuilder(actionContext.Request.RequestUri)
                {
                    Scheme = Uri.UriSchemeHttps,
                    Port = 443
                };

                actionContext.Response.Headers.Location = uriBuilder.Uri;
            }
        }
    }
}