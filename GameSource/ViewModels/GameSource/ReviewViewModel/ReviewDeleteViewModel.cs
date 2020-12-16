using GameSource.Models.GameSource;
using System.Collections;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.ReviewViewModel
{
    public class ReviewDeleteViewModel
    {
        public ReviewDeleteViewModel()
        {
            ReviewComments = new List<ReviewComment>();
        }

        public Review Review { get; set; }
        public IEnumerable<ReviewComment> ReviewComments { get; set; }
    }
}
