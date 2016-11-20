using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLabb.Models
{
    public class GalleryViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Minimum 5, Maximum 50 Characters")]
        public string GalleryName { get; set; }

        [Display(Name = "Created")]
        public DateTime DateCreated { get; set; }

        public int UserID { get; set; }

        public UserViewModel User { get; set; }

    }
}