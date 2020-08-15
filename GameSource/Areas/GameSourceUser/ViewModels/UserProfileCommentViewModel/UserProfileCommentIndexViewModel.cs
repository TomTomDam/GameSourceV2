using GameSource.Models.GameSourceUser;
using System.Collections.Generic;

namespace GameSource.Areas.GameSourceUser.ViewModels.UserProfileCommentViewModel
{
    public class UserProfileCommentIndexViewModel
    {
        public IEnumerable<UserProfileComment> UserProfileComments { get; set; }
    }
}
