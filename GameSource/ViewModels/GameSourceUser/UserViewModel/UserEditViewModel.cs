using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSourceUser.UserViewModel
{
    public class UserEditViewModel
    {
        public UserEditViewModel()
        {
            UserRole = new List<SelectListItem>();
        }

        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }

        public string Location { get; set; }

        [Display(Name = "Avatar File Path")]
        public string AvatarFilePath { get; set; }

        public IFormFile AvatarImage { get; set; }

        public string Description { get; set; }

        [Display(Name = "User Status")]
        public int UserStatusID { get; set; }

        [Display(Name = "User Role")]
        public int UserRoleID { get; set; }

        public List<SelectListItem> UserStatus { get; set; }

        public List<SelectListItem> UserRole { get; set; }
    }
}
