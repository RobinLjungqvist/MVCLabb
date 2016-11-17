using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;

namespace MVCLabb.Controllers
{
    public class PicturesController : Controller
    {
        private MVCLabbDB db = new MVCLabbDB();

        // GET: Pictures
        public ActionResult Index()
        {
            var pictures = db.Pictures.Where(p => p.@public).Include(p => p.Users).Include(p => p.Gallery);
            return RedirectToAction("Index", "Galleries");/*View(pictures.ToList());*/
        }

        // GET: Pictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pictures pictures = db.Pictures.Find(id);
            if (pictures == null)
            {
                return HttpNotFound();
            }
            return View(pictures);
        }

        // GET: Pictures/Create
        public ActionResult Create(int galleryID)
        {
            ViewBag.UserID = new SelectList(db.Users, "id", "FirstName");
            ViewBag.GalleryID = new SelectList(db.Galleries, "id", "GalleryName");
            return View();
        }

        // POST: Pictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Description,Path,UserID,DatePosted,DateEdited,public,GalleryID")] Pictures pictures)
        {
            if (ModelState.IsValid)
            {
                db.Pictures.Add(pictures);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "id", "FirstName", pictures.UserID);
            ViewBag.GalleryID = new SelectList(db.Galleries, "id", "GalleryName", pictures.GalleryID);
            return View(pictures);
        }

        // GET: Pictures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pictures pictures = db.Pictures.Find(id);
            if (pictures == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "id", "FirstName", pictures.UserID);
            ViewBag.GalleryID = new SelectList(db.Galleries, "id", "GalleryName", pictures.GalleryID);
            return View(pictures);
        }

        // POST: Pictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Description,Path,UserID,DatePosted,DateEdited,public,GalleryID")] Pictures pictures)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pictures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "id", "FirstName", pictures.UserID);
            ViewBag.GalleryID = new SelectList(db.Galleries, "id", "GalleryName", pictures.GalleryID);
            return View(pictures);
        }

        // GET: Pictures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pictures pictures = db.Pictures.Find(id);
            if (pictures == null)
            {
                return HttpNotFound();
            }
            return View(pictures);
        }

        // POST: Pictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pictures pictures = db.Pictures.Find(id);
            db.Pictures.Remove(pictures);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
