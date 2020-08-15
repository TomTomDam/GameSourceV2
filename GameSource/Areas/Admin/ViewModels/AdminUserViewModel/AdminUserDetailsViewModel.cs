using GameSource.Models.GameSourceUser;

namespace GameSource.Areas.Admin.ViewModels.UserViewModel
{
    public class AdminUserDetailsViewModel
    {
        public User User { get; set; }
        public int UserID { get; set; }
        public int UserRoleID { get; set; }
        public int UserStatusID { get; set; }
        public UserRole UserRole { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
