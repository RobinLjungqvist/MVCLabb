using MVCLabb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Models
{
    public class UserEntityModel
    {
        public UserEntityModel()
        {
            this.Comments = new HashSet<CommentEntityModel>();
            this.Pictures = new HashSet<PictureEntityModel>();
            this.Galleries = new HashSet<GalleryEntityModel>();
        }

        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public System.Guid guid { get; set; }

        public virtual ICollection<CommentEntityModel> Comments { get; set; }
        public virtual ICollection<PictureEntityModel> Pictures { get; set; }
        public virtual ICollection<GalleryEntityModel> Galleries { get; set; }
    }
}
