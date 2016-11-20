using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLabb.Areas.Account.Controllers
{
    public class MyPictureController : Controller
    {
        // GET: Account/MyPicture
        public ActionResult Index()
        {
            return View();
        }
    }
}