using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSourceUser.UserViewModel
{
    public class UserRegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation do not match. Please ensure your password is typed correctly in both fields.")]
        public string ConfirmPassword { get; set; }

        [StringLength(20)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
