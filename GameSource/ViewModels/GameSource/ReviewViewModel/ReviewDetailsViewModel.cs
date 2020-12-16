using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.ReviewViewModel
{
    public class ReviewDetailsViewModel
    {
        public ReviewDetailsViewModel()
        {
            ReviewComments = new List<ReviewComment>();
        }

        public Review Review { get; set; }
        public IEnumerable<ReviewComment> ReviewComments { get; set; }
    }
}
