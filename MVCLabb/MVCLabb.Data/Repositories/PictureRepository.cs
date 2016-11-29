using MVCLabb.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCLabb.Data.Models;

namespace MVCLabb.Data.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        public bool AddOrUpdate(PictureEntityModel picture)
        {
            try
            {
                using(var ctx = new DataContext())
                {
                    var pictureToUpdate = ctx.Pictures.Find(picture.id);
                    if(pictureToUpdate != null)
                    {
                        pictureToUpdate.Name = picture.Name;
                        pictureToUpdate.GalleryID = picture.GalleryID;
                        pictureToUpdate.Path = picture.Path;
                        pictureToUpdate.@public = picture.@public;
                        pictureToUpdate.Description = picture.Description;
                        pictureToUpdate.DateEdited = picture.DateEdited;
                        ctx.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var newPicture = new PictureEntityModel();
                        newPicture.Name = picture.Name;
                        newPicture.GalleryID = picture.GalleryID;
                        newPicture.Path = picture.Path;
                        newPicture.@public = picture.@public;
                        newPicture.Description = picture.Description;
                        newPicture.DateEdited = picture.DateEdited;
                        newPicture.DatePosted = picture.DatePosted;
                        newPicture.UserID = picture.UserID;
                        ctx.Pictures.Add(newPicture);
                        ctx.SaveChanges();
                        return true;
                    }
                }
            }
            catch(Exception e)
            {
                //Handle exceptions
            }
            return false;
        }

        public IEnumerable<PictureEntityModel> All()
        {
            using (var ctx = new DataContext())
            {
                var pictures = ctx.Pictures;
                return pictures.ToList();
            }
        }

        public PictureEntityModel ByID(int id)
        {
            using (var ctx = new DataContext())
            {
                var picture = ctx.Pictures.Find(id);
                return picture;
            }
        }

        public bool Delete(int id)
        {
            using (var ctx = new DataContext())
            {
                var picture = ctx.Pictures.Find(id);
                if(picture != null)
                {
                    ctx.Pictures.Remove(picture);
                    ctx.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
