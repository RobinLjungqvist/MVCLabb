using MVCLabb.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data
{
    public class DataContext : DbContext
    {
        public DbSet<UserEntityModel> Users { get; set; }
        public DbSet<GalleryEntityModel> Galleries { get; set; }

        public DbSet<CommentEntityModel> Comments { get; set; }

        public DbSet<PictureEntityModel> Pictures { get; set; }
    }
}
