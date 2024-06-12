using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhavyaModhiya_490_WebAPI.Session
{
    public class SessionManager
    {
        public static string jwt
        {
            get
            {
                return (string)(HttpContext.Current.Session["jwt"] == null ? 0 : HttpContext.Current.Session["jwt"]);
            }
            set
            {
                HttpContext.Current.Session["jwt"] = value;
            }
        }
    }
}