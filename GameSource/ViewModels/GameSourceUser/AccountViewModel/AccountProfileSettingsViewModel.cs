using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameSource.ViewModels.GameSourceUser.AccountViewModel
{
    public class AccountProfileSettingsViewModel
    {
        [StringLength(20)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Real Name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public IFormFile AvatarImage { get; set; }

        public string Description { get; set; }

        public int Age { get; set; }

        public string Location { get; set; }

        public string Role { get; set; }
    }
}
