using DataAccessLayer;
using MVCLabb.Models;
using MVCLabb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLabb.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
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
    }
}