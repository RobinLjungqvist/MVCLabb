using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLabb.Areas.Admin.Controllers
{
    public class UserEditController : Controller
    {
        // GET: Admin/UserEdit
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}