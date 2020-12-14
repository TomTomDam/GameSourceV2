using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.ReviewViewModel
{
    public class ReviewIndexViewModel
    {
        public ReviewIndexViewModel()
        {
            Reviews = new List<Review>();
        }

        public IEnumerable<Review> Reviews { get; set; }
    }
}
