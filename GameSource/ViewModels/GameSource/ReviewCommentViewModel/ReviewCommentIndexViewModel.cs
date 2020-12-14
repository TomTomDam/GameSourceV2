using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.ReviewCommentViewModel
{
    public class ReviewCommentIndexViewModel
    {
        public ReviewCommentIndexViewModel()
        {
            ReviewComments = new List<ReviewComment>();
        }

        public IEnumerable<ReviewComment> ReviewComments { get; set; }
    }
}
