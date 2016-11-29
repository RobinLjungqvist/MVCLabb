using MVCLabb.Data.Models;
using MVCLabb.Models;

namespace MVCLabb.Utilities
{
    public static class EntityModelMapper
    {
        #region Users
        public static UserEntityModel ModelToEntity(UserViewModel model)
        {
            var user = new UserEntityModel();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.guid = model.guid;
            user.Password = model.Password;
            user.id = model.id;

            return user;

        }

        public static UserViewModel EntityToModel(UserEntityModel entity)
        {
            var model = new UserViewModel();
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.Email = entity.Email;
            model.guid = entity.guid;
            model.id = entity.id;

            return model;


        }
        #endregion

        #region Gallery
        public static GalleryEntityModel ModelToEntity(GalleryViewModel model)
        {
            var entity = new GalleryEntityModel();
            entity.id = model.id;
            entity.GalleryName = model.GalleryName;
            entity.DateCreated = model.DateCreated;
            entity.UserID = model.UserID;

            return entity;
        }

        public static GalleryViewModel EntityToModel(GalleryEntityModel entity)
        {
            var model = new GalleryViewModel();
            model.id = entity.id;
            model.GalleryName = entity.GalleryName;
            model.DateCreated = entity.DateCreated;
            model.UserID = entity.UserID;
            model.User = EntityToModel(entity.User);

            return model;
        }
        #endregion

        #region Pictures


        public static PictureEntityModel ModelToEntity(PictureViewModel model)
        {
            var pic = new PictureEntityModel();
            pic.Name = model.Name;
            pic.id = model.id;
            pic.Path = model.Path;
            pic.UserID = model.UserID;
            pic.Description = model.Description;
            pic.DatePosted = model.DatePosted;
            pic.DateEdited = model.DateEdited;
            pic.GalleryID = model.GalleryID;
            pic.@public = model.IsPublicPicture;


            


            return pic;

        }

        public static PictureViewModel EntityToModel(PictureEntityModel entity)
        {
            var model = new PictureViewModel();
            model.Name = entity.Name;
            model.id = entity.id;
            model.Path = entity.Path;
            model.UserID = entity.UserID;
            model.Description = entity.Description;
            model.DatePosted = entity.DatePosted;
            model.DateEdited = entity.DateEdited;
            model.GalleryID = entity.GalleryID;
            model.IsPublicPicture = entity.@public;
            model.Uploader = entity.Users.FirstName + " " + entity.Users.LastName;
             

            return model;


        }

        #endregion

        #region Comment
        public static CommentEntityModel ModelToEntity(CommentViewModel model)
        {
            var entity = new CommentEntityModel();
            entity.Comment = model.Comment;
            entity.id = model.id;
            entity.PictureID = model.PictureID;
            entity.UserID = model.UserID;
            entity.Title = model.Title;
            entity.DateEdited = model.DateEdited;
            entity.DatePosted = model.DatePosted;

            return entity;
        }

        public static CommentViewModel EntityToModel(CommentEntityModel entity)
        {
            var model = new CommentViewModel();
            model.Comment = entity.Comment;
            model.id = entity.id;
            model.PictureID = entity.PictureID;
            model.Picture = EntityToModel(entity.Pictures);
            model.UserID = entity.UserID;
            model.User = EntityToModel(entity.Users);
            model.Title = entity.Title;
            model.DateEdited = entity.DateEdited;
            model.DatePosted = entity.DatePosted;



            return model;
        }




            #endregion


        }
}