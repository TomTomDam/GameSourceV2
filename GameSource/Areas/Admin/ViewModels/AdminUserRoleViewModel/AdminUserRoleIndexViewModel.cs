using GameSource.Models.GameSourceUser;
using System;
using System.Collections.Generic;

namespace GameSource.Areas.Admin.ViewModels.UserRoleViewModel
{
    public class AdminUserRoleIndexViewModel
    {
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
