using MVCLabb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repositories.Interfaces
{
    interface IUserRepository
    {
        IEnumerable<UserEntityModel> All();

        UserEntityModel ByID(int id);

        bool AddOrUpdate(UserEntityModel user);

        bool Delete(int id);

    }
}
