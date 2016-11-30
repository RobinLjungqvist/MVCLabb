using DataAccessLayer;
using MVCLabb.Data.Repositories;
using MVCLabb.Models;
using MVCLabb.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MVCLabb.Areas.Account.Controllers
{
    public class MyPageController : Controller
    {
        private UserRepository repo;

        public MyPageController()
        {
            this.repo = new UserRepository();
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var sid = identity.Claims.First(x => x.Type == ClaimTypes.Sid);
                int userID = int.Parse(sid.Value);


                    var userFromDB = repo.ByID(userID);

                    if(userFromDB != null)
                    {
                        var user = EntityModelMapper.EntityToModel(userFromDB);
                        return View(user);

                    }
                
            }

                return RedirectToAction("Login", "Auth");
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserViewModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var sid = identity.Claims.First(x => x.Type == ClaimTypes.Sid);
            int userID = int.Parse(sid.Value);

            ModelState.Remove("Password");

            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                var user = repo.ByID(userID);
                if (user != null && !repo.isEmailTaken(user.Email) || user.Email == model.Email)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    repo.AddOrUpdate(user);

                    identity.AddClaim(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));

                    var ctx = Request.GetOwinContext();
                    ctx.Authentication.SignIn(identity);

                    ViewData["Message"] = "User information updated";

                    return Content("User information updated!");
                }
                else
                {
                    model.Email = user.Email;
                    return Content("The email is already registered.");

                }

            }

            return Content("Couldn't update information");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string oldPassword, string Password, string newPassword2)
        {
            string success = string.Empty;

            if (Password != newPassword2 || Password == string.Empty)
            {
                success = "new passwords didn't match.";
                return Json(success);
            }

            if (User.Identity.IsAuthenticated)
            {

                int userID = int.Parse(Helpers.GetSid(User.Identity));
                var userFromDB = repo.ByID(userID);

                if (userFromDB.Password == oldPassword)
                {
                    userFromDB.Password = Password;
                    repo.AddOrUpdate(userFromDB);
                    success = "Update successful";
                    return Json(success);
                }
                success = "Wrong password";

            }

            success = success + " update failed.";

            return Content(success);
        }

        public ActionResult CheckEmailTaken(string email)
        {
            using(var ctx = new MVCLabbDB())
            {
                var emails = ctx.Users.Select(x => x.Email);

                if (emails.Contains(email))
                {
                    return Content("true");
                }
                else
                {
                    return Content("false");
                }
            }
        }
    }
}