using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Models
{
    public class CommentEntityModel
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int UserID { get; set; }
        public int PictureID { get; set; }
        public Nullable<System.DateTime> DatePosted { get; set; }
        public Nullable<System.DateTime> DateEdited { get; set; }

        public virtual PictureEntityModel Picture { get; set; }
        public virtual UserEntityModel User { get; set; }
    }
}
