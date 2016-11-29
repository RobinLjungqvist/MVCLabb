using MVCLabb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repositories.Interfaces
{
    interface IPictureRepository
    {
        IEnumerable<PictureEntityModel> All();

        PictureEntityModel ByID(int id);

        bool AddOrUpdate(PictureEntityModel picture);

        bool Delete(int id);
    }
}
