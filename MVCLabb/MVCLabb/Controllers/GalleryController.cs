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
                int? userID = int.Parse(Extensions.GetSid(identity));
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
                ViewData["GalleryInfo"] = "Gallery Created";
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}