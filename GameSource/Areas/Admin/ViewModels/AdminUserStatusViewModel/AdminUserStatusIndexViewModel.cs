using GameSource.Models.GameSourceUser;
using System;
using System.Collections.Generic;

namespace GameSource.Areas.Admin.ViewModels.UserStatusViewModel
{
    public class AdminUserStatusIndexViewModel
    {
        public IEnumerable<UserStatus> UserStatuses { get; set; }
    }
}
