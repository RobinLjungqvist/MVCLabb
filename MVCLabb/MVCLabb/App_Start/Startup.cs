using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace MVCLabb.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) // IAppBuilder fungerar som en entrypoint för request pipelinen.
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
            });
        }
        
    }
}