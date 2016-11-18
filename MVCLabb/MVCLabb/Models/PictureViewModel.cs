using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLabb.Models
{
    public class PictureViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public int UserID { get; set; }
        public DateTime? DatePosted { get; set; }
        public DateTime? DateEdited { get; set; }
        public bool IsPublicPicture { get; set; }
        public int GalleryID { get; set; }
        public string Uploader { get; set; }
    }
}