using GameSource.Models.GameSourceUser;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameSource.Areas.GameSourceUser.ViewModels.UserProfileViewModel
{
    public class UserProfileEditViewModel
    {
        public UserProfile UserProfile { get; set; }

        public List<SelectListItem> UserProfileVisibilityList { get; set; }

        public List<SelectListItem> UserProfileCommentPermissionList { get; set; }

        public User User { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Verify Email Address")]
        [Compare(nameof(EmailAddress), ErrorMessage = "Emails do not match. Please make sure you typed your email correctly.")]
        public string VerifiedEmailAddress { get; set; }
    }
}
