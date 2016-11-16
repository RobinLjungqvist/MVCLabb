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
                    new Claim(ClaimTypes.Email, userToLogin.Email),
                    new Claim(ClaimTypes.NameIdentifier, userToLogin.guid.ToString())
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


        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Users user)
        {
            user.guid = Guid.NewGuid();

            if (ModelState.IsValid)
            {
                using(var ctx = new MVCLabbDB())
                {
                    var newUser = ctx.Users.Create();
                    newUser.Email = user.Email;
                    newUser.FirstName = user.FirstName;
                    newUser.LastName = user.LastName;
                    newUser.Password = user.Password;
                    newUser.guid = Guid.NewGuid();
                    ctx.Users.Add(newUser);
                    ctx.SaveChanges();
                }
                return Redirect("/Auth/Login");
            }

            ModelState.AddModelError("","Something went wrong");
            return View(user);
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }
    }
}