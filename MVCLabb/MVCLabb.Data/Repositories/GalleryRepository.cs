using MVCLabb.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCLabb.Data.Models;
using System.Data.Entity;

namespace MVCLabb.Data.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {
        public bool AddOrUpdate(GalleryEntityModel gallery)
        {
            try { 
            using (var ctx = new DataContext())
            {
                    var galleryToUpdate = ctx.Galleries.Where(g => g.id == gallery.id)
                            .Include(g => g.Pictures)
                            .Include(g => g.User).
                            FirstOrDefault();
                if (galleryToUpdate != null)
                {
                    galleryToUpdate.GalleryName = gallery.GalleryName;
                    ctx.SaveChanges();
                    return true;
                }
                else
                {
                    var newGallery = new GalleryEntityModel();
                    newGallery.UserID = gallery.UserID;
                    newGallery.GalleryName = gallery.GalleryName;
                    newGallery.DateCreated = DateTime.Now;
                    ctx.Galleries.Add(newGallery);
                    ctx.SaveChanges();
                    return true;
                }
            }
            }
            catch(Exception e)
            {
                // handle exceptions
            }

            return false;

        }

        public IEnumerable<GalleryEntityModel> All()
        {
            using(var ctx = new DataContext())
            {
                var galleries = ctx.Galleries
                            .Include(g => g.Pictures)
                            .Include(g => g.User);      
                return galleries.ToList();
            }
        }

        public GalleryEntityModel ByID(int id)
        {
            using (var ctx = new DataContext())
            {
                var gallery = ctx.Galleries.Where(g => g.id == id)
                            .Include(g => g.Pictures)
                            .Include(g => g.User).
                            FirstOrDefault();
                return gallery;
            }
        }

        public bool Delete(int id)
        {
            using (var ctx = new DataContext())
            {
                var gallery = ctx.Galleries.Where(g => g.id == id)
                            .Include(g => g.Pictures)
                            .Include(g => g.User).
                            FirstOrDefault();

                if (gallery != null)
                {
                    ctx.Galleries.Remove(gallery);
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
