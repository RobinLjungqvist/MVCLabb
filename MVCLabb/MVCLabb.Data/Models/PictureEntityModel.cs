using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Models
{
    public class PictureEntityModel
    {
        public PictureEntityModel()
        {
            this.Comments = new HashSet<CommentEntityModel>();
        }

        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> DatePosted { get; set; }
        public Nullable<System.DateTime> DateEdited { get; set; }
        public bool @public { get; set; }
        public int GalleryID { get; set; }

        public virtual ICollection<CommentEntityModel> Comments { get; set; }
        public virtual UserEntityModel Users { get; set; }
        public virtual GalleryEntityModel Gallery { get; set; }
    }
}
