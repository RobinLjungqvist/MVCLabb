using DataAccessLayer;
using MVCLabb.Data.Repositories;
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
        CommentRepository repo;
        public CommentController()
        {
            this.repo = new CommentRepository();
        }
        // GET: Comment
        [AllowAnonymous]
        public ActionResult Comments(int pictureID)
        {
            var comments = new List<CommentViewModel>();


            var commentsFromDB = repo.All().Where(x => x.PictureID == pictureID);

            foreach (var comment in commentsFromDB)
            {
                comments.Add(EntityModelMapper.EntityToModel(comment));
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
                
                var newComment = EntityModelMapper.ModelToEntity(model);
                repo.AddOrUpdate(newComment);
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
                    var commentToRemove = repo.ByID(commentID);
                    if (commentToRemove != null)
                    {
                        repo.Delete(commentID);
                    }
                    return Content("Comment was removed.");
                }

            }
            return Content("Couldn't remove comment.");
        }
    }
}