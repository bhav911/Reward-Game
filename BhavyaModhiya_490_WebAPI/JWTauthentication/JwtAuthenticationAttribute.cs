using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Security.Principal;
using System.Web;
using System.Linq;

namespace BhavyaModhiya_490_WebAPI.JWTauthentication
{
    public class JwtAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            var cookies = request.Headers.GetCookies("jwt").FirstOrDefault();

            if (cookies != null)
            {
                var jwtCookie = cookies["jwt"];

                if (jwtCookie != null)
                {
                    var jwtToken = jwtCookie.Value;
                    var userName = Authentication.ValidateToken(jwtToken);
                    if (userName == null)
                    {
                        actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid token");
                        return;
                    }
                    var identity = new GenericIdentity(userName);
                    var principal = new GenericPrincipal(identity, null);
                    Thread.CurrentPrincipal = principal;
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                }
            }
            else
            {
                actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Missing token");
                return;
            }        

            base.OnActionExecuting(actionContext);
        }
    }
}