﻿using System.Web;
using System.Web.Mvc;

namespace BhavyaModhiya_490
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
