using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLabb.Controllers
{
    [AllowAnonymous] // Låter anonyma användare se sidan utan att vara authentiserade.
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}