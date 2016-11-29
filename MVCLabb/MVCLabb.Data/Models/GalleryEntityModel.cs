using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCLabb.Data.Models
{
    public class GalleryEntityModel
    {
        public GalleryEntityModel()
        {
            this.Pictures = new HashSet<PictureEntityModel>();
        }

        public int id { get; set; }
        public string GalleryName { get; set; }
        public System.DateTime DateCreated { get; set; }
        public int UserID { get; set; }

        public virtual UserEntityModel User { get; set; }
        public virtual ICollection<PictureEntityModel> Pictures { get; set; }
    }
}
