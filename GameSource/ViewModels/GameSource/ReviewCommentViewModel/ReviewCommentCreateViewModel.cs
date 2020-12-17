using GameSource.Models.GameSource;
using GameSource.Models.GameSourceUser;

namespace GameSource.ViewModels.GameSource.ReviewCommentViewModel
{
    public class ReviewCommentCreateViewModel
    {
        public ReviewComment ReviewComment { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
