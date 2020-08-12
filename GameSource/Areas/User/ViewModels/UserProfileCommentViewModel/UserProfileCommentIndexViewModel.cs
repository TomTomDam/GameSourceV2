using GameSource.Models.GameSourceUser;
using System.Collections.Generic;

namespace GameSource.Areas.User.ViewModels.UserProfileCommentViewModel
{
    public class UserProfileCommentIndexViewModel
    {
        public IEnumerable<UserProfileComment> UserProfileComments { get; set; }
    }
}
