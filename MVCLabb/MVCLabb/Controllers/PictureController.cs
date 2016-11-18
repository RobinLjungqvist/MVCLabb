using DataAccessLayer;
using MVCLabb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Utilities;

namespace MVCLabb.Controllers
{
    public class PictureController : Controller
    {
        // GET: Picture
        public ActionResult Show(GalleryViewModel model)
        {
            var pictures = new List<PictureViewModel>();
            using(var ctx = new MVCLabbDB())
            {
                var picturesFromDB = ctx.Pictures.Where(p => p.GalleryID == model.id).ToList();
                foreach (var pic in picturesFromDB)
                {
                    pictures.Add(EntityModelMapper.EntityToModel(pic));
                }
            }
            ViewBag.GalleryName = model.GalleryName;
            return View(pictures);
        }
        public ActionResult Details(PictureViewModel picture)
        {
            return View(picture);
        }
    }
}