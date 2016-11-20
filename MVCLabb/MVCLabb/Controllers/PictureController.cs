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

namespace MVCLabb.Controllers
{
    [Authorize]
    public class PictureController : Controller
    {
        // GET: Picture
        [AllowAnonymous]
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
        [AllowAnonymous]
        public ActionResult Details(PictureViewModel picture)
        {
            return View(picture);
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
            model.UserID = int.Parse(MVCLabb.Utilities.Extensions.GetSid(User.Identity));
            model.DatePosted = DateTime.Now;

            string pictureFolder = Server.MapPath("../../Images");

            var path = string.Empty;
            var fileName = string.Empty;
            if (photo != null && photo.ContentLength > 0)
            {


                fileName = Path.GetFileName(photo.FileName);
                path = Path.Combine(pictureFolder, fileName);
                photo.SaveAs(path);


            }


            model.Path = "~/Images/" + fileName;




            if (ModelState.IsValid)
            {
                var newPicture = EntityModelMapper.ModelToEntity(model);
                using(var ctx = new MVCLabbDB())
                {
                    ctx.Pictures.Add(newPicture);
                    ctx.SaveChanges();
                }
                return RedirectToAction("ViewGallery", "Gallery", new { id = model.GalleryID });
            }
            return View(model);
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
                using (var ctx = new MVCLabbDB())
                {
                    var picToRemove = ctx.Pictures.Find(id);
                    if (picToRemove != null)
                    {
                        filePath = Request.MapPath(picToRemove.Path);
                        FileInfo file = new FileInfo(filePath);

                        ctx.Pictures.Remove(picToRemove);
                        ctx.SaveChanges();
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                }
                return RedirectToAction("ViewGallery", "Gallery", new { id = GalleryID }); 
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Edit(PictureViewModel model)
        {
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
                FileInfo fileToDelete = new FileInfo(model.Path);
                if (fileToDelete.Exists)
                {
                    fileToDelete.Delete();
                }

                fileName = Path.GetFileName(file.FileName);
                path = Path.Combine(pictureFolder, fileName);
                file.SaveAs(path);

                model.Path = "~/Images/" + fileName;
            }
            model.DateEdited = DateTime.Now;
            using(var ctx = new MVCLabbDB())
            {
                var picToUpdate = ctx.Pictures.Find(model.id);
                picToUpdate.Name = model.Name;
                picToUpdate.Description = model.Description;
                picToUpdate.Path = model.Path;
                picToUpdate.DateEdited = model.DateEdited;
                

                ctx.Entry(picToUpdate).State = EntityState.Modified;
                ctx.SaveChanges();


            }

            return View(model);
        }
    }
}