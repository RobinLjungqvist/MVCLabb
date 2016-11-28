using DataAccessLayer;
using MVCLabb.Models;
using MVCLabb.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MVCLabb.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        // GET: Gallery
        [AllowAnonymous]
        public ActionResult Index()
        {
            var galleries = new List<GalleryViewModel>();

            using (var ctx = new MVCLabbDB())
            {
                var galleriesFromDB = ctx.Galleries.ToList();

                foreach (var g in galleriesFromDB)
                {
                    galleries.Add(EntityModelMapper.EntityToModel(g));
                }

            }
            return View(galleries);
        }
        [AllowAnonymous]
        public ActionResult ViewGallery(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            GalleryViewModel galleryToView = null;

            using(var ctx = new MVCLabbDB())
            {
                galleryToView = EntityModelMapper.EntityToModel(ctx.Galleries.Find(id));
            }

            if(galleryToView != null)
            {
                return View(galleryToView);
            }

            return RedirectToAction("Index");

        }

        public ActionResult Create()
        {

            return View(new GalleryViewModel());
        }
        [HttpPost]
        public ActionResult Create(GalleryViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;
                int? userID = int.Parse(Helpers.GetSid(identity));
                if (userID != null)
                {
                    model.DateCreated = DateTime.Now;
                    model.UserID = (int)userID;
                    using (var ctx = new MVCLabbDB())
                    {
                        var newGallery = EntityModelMapper.ModelToEntity(model);
                        ctx.Galleries.Add(newGallery);
                        ctx.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Gallery");
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult Delete(int galleryID)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userID = int.Parse(Helpers.GetSid(User.Identity));

                using (var ctx = new MVCLabbDB())
                {
                    var galleryToRemove = ctx.Galleries.Find(galleryID);

                    if (galleryToRemove.UserID == userID)
                    {

                        var picturesToRemove = ctx.Pictures.Where(p => p.UserID == userID && p.GalleryID == galleryID);

                        if(picturesToRemove != null)
                        {
                            foreach (var picture in picturesToRemove)
                            {
                                var commentsToRemove = ctx.Comments.Where(c => c.PictureID == picture.id);
                                ctx.Comments.RemoveRange(commentsToRemove);

                                var filePath = Request.MapPath(picture.Path);
                                FileInfo file = new FileInfo(filePath);
                                if (file.Exists)
                                {
                                    file.Delete();
                                }
                            }
                            

                            ctx.Pictures.RemoveRange(picturesToRemove);

                        }


                        ctx.Galleries.Remove(galleryToRemove);
                        ctx.SaveChanges();
                        return Content("Gallery deleted!");
                    }
                }
            }
            return Content("Couldn't delete gallery");
            
        }

        public ActionResult GalleryList()
        {
            var galleryList = new List<GalleryViewModel>();
            using (var ctx = new MVCLabbDB())
            {
                foreach (var gallery in ctx.Galleries)
                {
                    galleryList.Add(EntityModelMapper.EntityToModel(gallery));
                }
            }

            return PartialView("_GalleryListView", galleryList);
        }
    }
}