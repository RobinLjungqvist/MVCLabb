using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using DataAccessLayer;

namespace MVCLabb.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            Users userToLogin = null;
            using (var ctx = new MVCLabbDB())
            {
                userToLogin = ctx.Users.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            }

            if (userToLogin != null)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, (userToLogin.FirstName + " " + userToLogin.LastName)),
                    new Claim(ClaimTypes.Email, userToLogin.Email)
                },
                        "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect("/Home/Index");

            }
            ModelState.AddModelError("","Invalid Email or Password");
            return View(user);
        }
    }
}