using DataAccessLayer;
using MVCLabb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLabb.Utilities
{
    public static class EntityModelMapper
    {
        #region Users
        public static Users ModelToEntity(UserViewModel model)
        {
            var user = new Users();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.guid = model.guid;
            user.Password = model.Password;
            user.id = model.id;

            return user;

        }

        public static UserViewModel EntityToModel(Users entity)
        {
            var model = new UserViewModel();
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.Email = entity.Email;
            model.guid = entity.guid;
            model.Password = entity.Password;
            model.id = entity.id;

            return model;


        }
        #endregion

        #region Gallery
        public static Gallery ModelToEntity(GalleryViewModel model)
        {
            var entity = new Gallery();
            entity.id = model.id;
            entity.GalleryName = model.GalleryName;
            entity.DateCreated = model.DateCreated;
            entity.UserID = model.UserID;

            return entity;
        }

        public static GalleryViewModel EntityToModel(Gallery entity)
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


    }
}