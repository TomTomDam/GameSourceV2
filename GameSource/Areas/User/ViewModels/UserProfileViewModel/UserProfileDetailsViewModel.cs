using GameSource.Models.GameSourceUser;

namespace GameSource.Areas.User.ViewModels.UserProfileViewModel
{
    public class UserProfileDetailsViewModel
    {
        public UserProfile UserProfile { get; set; }

        public Models.GameSourceUser.User User { get; set; }
    }
}
