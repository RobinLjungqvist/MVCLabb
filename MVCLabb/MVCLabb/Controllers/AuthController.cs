using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using DataAccessLayer;
using MVCLabb.Models;
using MVCLabb.Utilities;

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
        public ActionResult Login(UserViewModel model)
        {
            Users userToLogin = null;
            using (var ctx = new MVCLabbDB())
            {
                userToLogin = ctx.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            }

            if (userToLogin != null)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, (userToLogin.FirstName + " " + userToLogin.LastName)),
                    new Claim(ClaimTypes.Email, userToLogin.Email),
                    new Claim(ClaimTypes.Sid, userToLogin.id.ToString()),
                    new Claim(ClaimTypes.Role, UserIsAdmin(userToLogin) ? "Admin" : "User")
                    
                },
                        "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                

                authManager.SignIn(identity);

                return Redirect("/Home/Index");

            }
            ModelState.AddModelError("","Invalid Email or Password");
            return View(model);
        }


        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(UserViewModel user)
        {
            user.guid = Guid.NewGuid();
            using (var ctx = new MVCLabbDB())
            {
                if (ModelState.IsValid && ctx.Users.FirstOrDefault(x=> x.Email.Contains(user.Email)) == null)
                {


                    var newUser = EntityModelMapper.ModelToEntity(user);
                    ctx.Users.Add(newUser);
                    ctx.SaveChanges();
                    return Redirect("/Auth/Login");
                }

            }


            ModelState.AddModelError("", "Something went wrong");
            return View(user);
        }
        

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }
        private bool UserIsAdmin(Users user)
        {

            if(user.guid == Guid.Parse("{3027308A-5C93-4694-869A-BA40F042F94C}"))
            {
                return true;
            }

            return false; 

        }
    }
}