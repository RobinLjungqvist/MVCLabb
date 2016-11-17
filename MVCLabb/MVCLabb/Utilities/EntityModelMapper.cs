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

        public static UserViewModel ModelToEntity(Users entitity)
        {
            var model = new UserViewModel();
            model.FirstName = entitity.FirstName;
            model.LastName = entitity.LastName;
            model.Email = entitity.Email;
            model.guid = entitity.guid;
            model.Password = entitity.Password;
            model.id = entitity.id;

            return model;


        }
    }
}