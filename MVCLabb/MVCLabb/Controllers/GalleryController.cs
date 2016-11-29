using DataAccessLayer;
using MVCLabb.Data.Repositories;
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
        private GalleryRepository repo;

        public GalleryController()
        {
            this.repo = new GalleryRepository();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var galleries = new List<GalleryViewModel>();

            var galleriesFromDB = repo.All();

            if (galleriesFromDB != null)
            {
                foreach (var gallery in galleriesFromDB)
                {
                    var galleryToAdd = EntityModelMapper.EntityToModel(gallery);
                    galleries.Add(galleryToAdd);
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

            var galleryID = (int)id;

            galleryToView = EntityModelMapper.EntityToModel(repo.ByID(galleryID));
            

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

                    var entity = EntityModelMapper.ModelToEntity(model);
                    repo.AddOrUpdate(entity);
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


                var galleryToRemove = repo.ByID(galleryID);

                if (galleryToRemove.UserID == userID)
                {

                    if (galleryToRemove.Pictures != null)
                    {
                        foreach (var picture in galleryToRemove.Pictures)
                        {
                            var filePath = Request.MapPath(picture.Path);
                            FileInfo file = new FileInfo(filePath);
                            if (file.Exists)
                            {
                                file.Delete();
                            }
                        }
                    }


                    repo.Delete(galleryToRemove.id);
                    return Content("Gallery deleted!");
                }
            }
            return Content("Couldn't delete gallery");
            
        }

        public ActionResult GalleryList()
        {
            var galleryList = new List<GalleryViewModel>();
                foreach (var gallery in repo.All())
                {
                    galleryList.Add(EntityModelMapper.EntityToModel(gallery));
                }


            return PartialView("_GalleryListView", galleryList);
        }
    }
}