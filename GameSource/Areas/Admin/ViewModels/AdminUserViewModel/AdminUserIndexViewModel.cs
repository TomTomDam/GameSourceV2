using GameSource.Models.GameSourceUser;
using System.Collections.Generic;

namespace GameSource.Areas.Admin.ViewModels.UserViewModel
{
    public class AdminUserIndexViewModel
    {
        public IEnumerable<Models.GameSourceUser.User> Users { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
        public IEnumerable<UserStatus> UserStatuses { get; set; }
    }
}
