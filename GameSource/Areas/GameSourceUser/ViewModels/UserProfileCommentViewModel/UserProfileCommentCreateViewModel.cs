using GameSource.Models.GameSourceUser;

namespace GameSource.Areas.GameSourceUser.ViewModels.UserProfileCommentViewModel
{
    public class UserProfileCommentCreateViewModel
    {
        public UserProfile UserProfile { get; set; }

        public User Author { get; set; }
        public UserProfileComment UserProfileComment { get; set; }
    }
}
