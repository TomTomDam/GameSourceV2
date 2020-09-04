using GameSource.Models.GameSourceUser;
using System.Collections.Generic;

namespace GameSource.Areas.Admin.ViewModels.AdminUserViewModel
{
    public class AdminUserIndexViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
        public IEnumerable<UserStatus> UserStatuses { get; set; }
    }
}
