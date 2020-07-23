using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameSource.Areas.Admin.ViewModels.UserViewModel
{
    public class AdminUserEditViewModel
    {
        public AdminUserEditViewModel()
        {
            UserRoles = new List<SelectListItem>();
            UserStatuses = new List<SelectListItem>();
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

        [Display(Name = "Avatar Image")]
        public IFormFile AvatarImage { get; set; }

        public string Description { get; set; }

        [Display(Name = "User Status")]
        public int UserStatusID { get; set; }

        [Display(Name = "User Role")]
        public int UserRoleID { get; set; }

        public List<SelectListItem> UserStatuses { get; set; }

        public List<SelectListItem> UserRoles { get; set; }
    }
}
