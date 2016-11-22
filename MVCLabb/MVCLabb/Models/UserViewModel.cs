using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLabb.Models
{
    public class UserViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimum 2 Characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimum 2 Characters")]
        [Display(Name = "Last Name")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Minimum 10 Characters")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Not a valid Email adress")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Max 20 and Minimum 8 Characters")]
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*[a-z])(?=.*[A-Z])(?=.*\\d)).+$", ErrorMessage = "Password must contain a combination of upper and lowercase and atleast one number.")]
        public string Password { get; set; }
        public Guid guid { get; set; }

        [Display(Name = "Name")]
        public string FullName { get { return FirstName + " " + LastName; } }


    }
}