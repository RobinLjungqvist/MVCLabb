using MVCLabb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Repositories.Interfaces
{
    interface ICommentRepository
    {
        IEnumerable<CommentEntityModel> All();

        CommentEntityModel ByID(int id);

        bool AddOrUpdate(CommentEntityModel comment);

        bool Delete(int id);
    }
}
