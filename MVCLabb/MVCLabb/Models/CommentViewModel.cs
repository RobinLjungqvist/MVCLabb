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
        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Minimum 3, Maximum 50 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(140,ErrorMessage ="Max 140 Characters!")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        public int UserID { get; set; }
        public int PictureID { get; set; }

        [Display(Name = "Posted")]
        public DateTime? DatePosted { get; set; }
        [Display(Name = "Edited")]
        public DateTime? DateEdited { get; set; }

        public PictureViewModel Picture { get; set; }
        public UserViewModel User { get; set; }
    }
}