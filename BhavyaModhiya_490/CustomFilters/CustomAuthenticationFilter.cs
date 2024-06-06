using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhavyaModhiya_490.CustomFilters
{
    public class CustomAuthenticationFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return HttpContext.Current.Session["UserID"] != null && HttpContext.Current.Session["Email"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary()
            {
                {"controller", "Login" },
                {"action","SignIn" }
            });
        }
    }
}