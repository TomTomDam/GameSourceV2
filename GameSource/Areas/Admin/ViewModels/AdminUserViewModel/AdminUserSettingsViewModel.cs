using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GameSource.Areas.Admin.ViewModels.AdminUserViewModel
{
    public class AdminUserSettingsViewModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [StringLength(20)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Real Name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public string AvatarFilePath { get; set; }

        [Display(Name = "Upload Image")]
        public IFormFile AvatarImage { get; set; }

        public string Description { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public int Age { get; set; }

        public string Location { get; set; }

        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}
