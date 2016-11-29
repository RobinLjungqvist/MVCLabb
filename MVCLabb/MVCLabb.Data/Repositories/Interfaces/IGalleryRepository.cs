using MVCLabb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repositories.Interfaces
{
    interface IGalleryRepository
    {
        IEnumerable<GalleryEntityModel> All();

        GalleryEntityModel ByID(int id);

        bool AddOrUpdate(GalleryEntityModel gallery);

        bool Delete(int id);
    }
}
