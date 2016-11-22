using DataAccessLayer;
using MVCLabb.Models;
using MVCLabb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MVCLabb.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        // GET: Comment
        [AllowAnonymous]
        public ActionResult Comments(PictureViewModel picture)
        {
            var comments = new List<CommentViewModel>();

            using(var ctx = new MVCLabbDB())
            {
                var commentsFromDB = ctx.Comments.Where(c => c.PictureID == picture.id).OrderByDescending(c => c.DatePosted);
                foreach (var comment in commentsFromDB)
                {
                    comments.Add(EntityModelMapper.EntityToModel(comment));
                }
            }

            return View(comments);
        }

        public ActionResult NewComment(PictureViewModel picture)
        {
            var newComment = new CommentViewModel();
            newComment.PictureID = picture.id;

            return View(newComment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewComment(CommentViewModel model)
        {

            var identity = (ClaimsIdentity)User.Identity;
            var sid = identity.Claims.First(x => x.Type == ClaimTypes.Sid);
            model.UserID = int.Parse(sid.Value);

            model.DatePosted = DateTime.Now;
            

            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                var newComment = new Comments();
                newComment = EntityModelMapper.ModelToEntity(model);

                using (var ctx = new MVCLabbDB())
                {
                    ctx.Comments.Add(newComment);
                    ctx.SaveChanges();
                } 

            }

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}