using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhavyaModhiya_490.Session
{
    public class UserSession
    {
        public static int UserID
        {
            get
            {
                return (int)(HttpContext.Current.Session["UserID"] == null ? 0 : HttpContext.Current.Session["UserID"]);
            }
            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }
        }

        public static string Email
        {
            get
            {
                return (string)(HttpContext.Current.Session["Email"] == null ? 0 : HttpContext.Current.Session["Email"]);
            }
            set
            {
                HttpContext.Current.Session["Email"] = value;
            }
        }
    }
}