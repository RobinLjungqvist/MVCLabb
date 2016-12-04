using DataAccessLayer;
using MVCLabb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLabb.Utilities;
using System.IO;
using System.Data.Entity;
using System.Threading;
using MVCLabb.Data.Repositories;

namespace MVCLabb.Controllers
{
    
    [Authorize]
    public class PictureController : Controller
    {
        private PictureRepository repo;
        public PictureController()
        {
            this.repo = new PictureRepository();
        }

        // GET: Picture
        [AllowAnonymous]
        public ActionResult Show(GalleryViewModel model)
        {
            var pictures = new List<PictureViewModel>();
            var picturesFromDB = repo.All().Where(x => x.GalleryID == model.id);
                foreach (var pic in picturesFromDB)
                {
                    pictures.Add(EntityModelMapper.EntityToModel(pic));
                }
            ViewBag.GalleryName = model.GalleryName;
            return View(pictures);
        }
        [AllowAnonymous]
        public ActionResult Details(PictureViewModel picture)
        {
            var pictureModel = EntityModelMapper.EntityToModel(repo.ByID(picture.id));
            return View(pictureModel);
        }

        public ActionResult Create(GalleryViewModel model)
        {
            var pic = new PictureViewModel();
            pic.GalleryID = model.id;
            return View(pic);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PictureViewModel model, HttpPostedFileBase photo)
        {
            Thread.Sleep(5000);
            model.UserID = int.Parse(MVCLabb.Utilities.Helpers.GetSid(User.Identity));
            model.DatePosted = DateTime.Now;
            string pictureFolder = Server.MapPath("~/Images");

            var path = string.Empty;
            var fileName = string.Empty;
            if (photo != null && photo.ContentLength > 0)
            {

                fileName = model.UserID + model.GalleryID + Path.GetFileName(photo.FileName);
                if (!Helpers.IsFilePicture(fileName))
                {
                    return Content("The file must be a picture in the format png, jpg or jpeg");
                }
                path = Path.Combine(pictureFolder, fileName);
                photo.SaveAs(path);


            }
            else
            {
                return Content("You must choose a file!");
            }


            model.Path = "~/Images/" + fileName;




            if (ModelState.IsValid)
            {
                var newPicture = EntityModelMapper.ModelToEntity(model);
                repo.AddOrUpdate(newPicture);
                return Content("Added picture to gallery!");
                //return RedirectToAction("ViewGallery", "Gallery", new { id = model.GalleryID });
            }
            return Content("Something went wrong couldn't add the picture");
        }

        public ActionResult Delete(PictureViewModel model)
        {
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, int GalleryID)
        {
            string filePath = string.Empty;

            if (id != null && User.Identity.IsAuthenticated)
            {
                int pictureID = (int)id;
                var picToRemove = repo.ByID(pictureID);
                    if (picToRemove != null)
                    {
                        filePath = Request.MapPath(picToRemove.Path);
                        FileInfo file = new FileInfo(filePath);

                        repo.Delete(picToRemove.id);

                       
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                
                return Json(Url.Action("ViewGallery", "Gallery", new { id = GalleryID }));
            }
            return Json(Request.UrlReferrer.ToString());
        }

        public ActionResult Edit(int id)
        {
            var model = new PictureViewModel();

                var picFromDB = repo.ByID(id);
                if (picFromDB != null)
                {
                    model = EntityModelMapper.EntityToModel(picFromDB);
                }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult EditPicture(PictureViewModel model, HttpPostedFileBase file)
        {
            string pictureFolder = Server.MapPath("../../Images");

            var path = string.Empty;
            var fileName = string.Empty;

            if (file != null && file.ContentLength > 0)
            {

                fileName = model.UserID + model.GalleryID + Path.GetFileName(file.FileName);
                path = Path.Combine(pictureFolder, fileName);

                file.SaveAs(path);

                model.Path = "~/Images/" + fileName;

                RemoveOldFileIfExists(model);

            }
            model.DateEdited = DateTime.Now;
            if (ModelState.IsValid)
            {
                var picToUpdate = EntityModelMapper.ModelToEntity(model);
                repo.AddOrUpdate(picToUpdate);
                return RedirectToAction("Details", "Picture", model);
            }

            ModelState.AddModelError("", "Couldn't update information");
            return View(model);

        }
    
        private void RemoveOldFileIfExists(PictureViewModel picture)
        {
            var oldpicture = repo.ByID(picture.id);
            if(oldpicture.Path != picture.Path)
            {
                var oldPhysicalPath = Request.MapPath(oldpicture.Path);
                FileInfo oldfile = new FileInfo(oldPhysicalPath);
                if (oldfile.Exists)
                {
                    oldfile.Delete();
                }
            }
        }
    }
}