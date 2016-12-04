using MVCLabb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserEntityModel> All();

        UserEntityModel ByID(int id);

        bool AddOrUpdate(UserEntityModel user);

        bool Delete(int id);

        UserEntityModel LoginUser(string email, string password);

        bool isEmailTaken(string email);
    }
}
