using GameSource.Models.GameSourceUser;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GameSource.Areas.GameSourceUser.ViewModels.UserProfileViewModel
{
    public class UserProfileEditViewModel
    {
        public UserProfile UserProfile { get; set; }

        public List<SelectListItem> UserProfileVisibilityList { get; set; }

        public List<SelectListItem> UserProfileCommentPermissionList { get; set; }

        public User User { get; set; }
    }
}
