using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using DataAccessLayer;
using MVCLabb.Models;
using MVCLabb.Utilities;
using MVCLabb.Data.Repositories;
using MVCLabb.Data.Models;

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
            var repo = new UserRepository();
            UserEntityModel userToLogin = null;

            userToLogin = repo.LoginUser(model.Email, model.Password);

            if (userToLogin != null)
            {

                //Refaktorera till en egen metod
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
            var repo = new UserRepository();
            user.guid = Guid.NewGuid();

                if (ModelState.IsValid && !repo.isEmailTaken(user.Email))
                {


                    var newUser = EntityModelMapper.ModelToEntity(user);
                    
                    repo.AddOrUpdate(newUser);

                    return Redirect("/Auth/Login");
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
        private bool UserIsAdmin(UserEntityModel user)
        {

            if(user.guid == Guid.Parse("{3027308A-5C93-4694-869A-BA40F042F94C}"))
            {
                return true;
            }

            return false; 

        }
    }
}