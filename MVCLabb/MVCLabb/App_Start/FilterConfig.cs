using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLabb.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()); // Hanterar fel som skickas av filtren
            filters.Add(new AuthorizeAttribute()); // Hanterar authorization för controllers och actions.
        }
    }
}