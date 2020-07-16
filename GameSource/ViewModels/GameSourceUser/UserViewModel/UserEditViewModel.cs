using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int? Age { get; set; }

        public string Location { get; set; }

        public string AvatarFilePath { get; set; }

        public IFormFile AvatarImage { get; set; }

        public string Description { get; set; }

        public int UserStatusID { get; set; }

        public int UserRoleID { get; set; }

        public List<SelectListItem> UserStatus { get; set; }

        public List<SelectListItem> UserRole { get; set; }
    }
}
