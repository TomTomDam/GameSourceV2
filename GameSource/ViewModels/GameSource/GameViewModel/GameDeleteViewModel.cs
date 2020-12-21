using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.GameViewModel
{
    public class GameDeleteViewModel
    {
        public GameDeleteViewModel()
        {
            Reviews = new List<Review>();
        }

        public Game Game { get; set; }
        public Genre Genre { get; set; }
        public Developer Developer { get; set; }
        public Publisher Publisher { get; set; }
        public Platform Platform { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
