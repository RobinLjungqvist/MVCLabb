using MVCLabb.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCLabb.Data.Models;

namespace MVCLabb.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public bool AddOrUpdate(UserEntityModel user)
        {
            try { 
            using(var ctx = new DataContext())
            {
                    var userToUpdate = ctx.Users.Find(user.id);
                   
                if(userToUpdate != null)
                {
                    userToUpdate.FirstName = user.FirstName;
                    userToUpdate.LastName = user.LastName;
                    userToUpdate.Email = user.Email;
                    userToUpdate.Password = user.Password;
                    ctx.SaveChanges();
                        return true;
                }
                else
                {
                    var newUser = new UserEntityModel();
                    newUser.FirstName = user.FirstName;
                    newUser.LastName = user.LastName;
                    newUser.Email = user.Email;
                    newUser.Password = user.Password;
                    newUser.guid = Guid.NewGuid();
                    ctx.Users.Add(newUser);
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

        public IEnumerable<UserEntityModel> All()
        {
            using(var ctx = new DataContext())
            {
                var users = ctx.Users;
                return users.ToList();
            }
        }

        public UserEntityModel ByID(int id)
        {
            using(var ctx = new DataContext())
            {
                var user = ctx.Users.Find(id);
                return user;
            }
        }

        public bool Delete(int id)
        {
            using (var ctx = new DataContext())
            {
                var user = ctx.Users.Find(id);
                if (user != null)
                {
                    ctx.Users.Remove(user);
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool isEmailTaken(string email)
        {
            using(var ctx = new DataContext())
            {
                if(ctx.Users.Where(x=>x.Email == email).Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public UserEntityModel LoginUser(string email, string password)
        {
            using(var ctx = new DataContext())
            {
                var userToLogin = ctx.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                return userToLogin;
            }
        }
    }
}
