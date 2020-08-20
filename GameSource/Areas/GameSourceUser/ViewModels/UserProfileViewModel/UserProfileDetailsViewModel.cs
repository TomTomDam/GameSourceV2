﻿using GameSource.Models.GameSourceUser;
using System.Collections.Generic;

namespace GameSource.Areas.GameSourceUser.ViewModels.UserProfileViewModel
{
    public class UserProfileDetailsViewModel
    {
        public UserProfileDetailsViewModel()
        {
            UserProfileComments = new List<UserProfileComment>();
        }

        public UserProfile UserProfile { get; set; }

        public User User { get; set; }

        public List<UserProfileComment> UserProfileComments { get; set; }
    }
}
