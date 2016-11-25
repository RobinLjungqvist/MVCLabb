using DataAccessLayer;
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
        // GET: Account/MyPage
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var sid = identity.Claims.First(x => x.Type == ClaimTypes.Sid);
                int? userID = int.Parse(sid.Value);


                if(userID != null)
                {
                    var user = new UserViewModel();
                    var userFromDB = new Users();
                    using(var ctx = new MVCLabbDB())
                    {
                      userFromDB =  ctx.Users.FirstOrDefault(u => u.id == userID);

                    }

                    if(userFromDB != null)
                    {
                        user = EntityModelMapper.EntityToModel(userFromDB);
                        return View(user);

                    }
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
            int? userID = int.Parse(sid.Value);

            ModelState.Remove("Password");

            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                using(var ctx = new MVCLabbDB())
                {
                    var user = ctx.Users.FirstOrDefault(u => u.id == userID);
                    if(user != null && ctx.Users.Where(x=>x.Email.Contains(model.Email)).Count() == 0 || user.Email == model.Email)
                    {
                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.Email = model.Email;
                        ctx.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        ctx.SaveChanges();

                        ViewData["Message"] = "User information updated";

                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("", "The email is already registered.");
                        model.Email = user.Email;
                    }

                }
                ModelState.AddModelError("", "Couldn't update information");
                return View(model);
            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string oldPassword, string Password, string newPassword2)
        {
            string success = string.Empty;

            if(Password != newPassword2 || Password == string.Empty )
            {
                success = "new passwords didn't match.";
                return Json(success);
            }

            if (User.Identity.IsAuthenticated)
            {
                using (var ctx = new MVCLabbDB())
                {
                    int userID = int.Parse(Helpers.GetSid(User.Identity));
                    var userFromDB = ctx.Users.FirstOrDefault(x => x.id == userID);

                    if(userFromDB.Password == oldPassword)
                    {
                        userFromDB.Password = Password;
                        ctx.Entry(userFromDB).State = EntityState.Modified;
                        ctx.SaveChanges();
                        success = "Update successful";
                        return Json(success);
                    }
                    success = "Wrong password";
                }

            }

                success = success + " update failed.";

            return Content(success);
        }
    }
}