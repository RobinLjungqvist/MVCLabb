using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLabb.Models
{
    public class CommentViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Title Required")]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="Max 140 Characters!")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        public int UserID { get; set; }
        public int PictureID { get; set; }
        public DateTime? DatePosted { get; set; }
        public DateTime? DateEdited { get; set; }

        public PictureViewModel Picture { get; set; }
        public UserViewModel User { get; set; }
    }
}