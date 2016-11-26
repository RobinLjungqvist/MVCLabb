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
        public ActionResult Comments(int pictureID)
        {
            var comments = new List<CommentViewModel>();

            using(var ctx = new MVCLabbDB())
            {
                var commentsFromDB = ctx.Comments.Where(c => c.PictureID == pictureID).OrderByDescending(c => c.DatePosted);
                foreach (var comment in commentsFromDB)
                {
                    comments.Add(EntityModelMapper.EntityToModel(comment));
                }
            }

            return PartialView(comments);
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
                return Content("Comment added");

            }

            return Content("Couldn't add comment");
        }
        [HttpPost]
        public ActionResult Delete(int commentID)
        {
            if (User.Identity.IsAuthenticated)
            {


                using (var ctx = new MVCLabbDB())
                {
                    var commentToRemove = ctx.Comments.Find(commentID);
                    if (commentToRemove != null)
                    {
                        ctx.Comments.Remove(commentToRemove);
                        ctx.SaveChanges();
                    }
                    return Content("Comment was removed.");
                }

            }
            return Content("Couldn't remove comment.");
        }
    }
}