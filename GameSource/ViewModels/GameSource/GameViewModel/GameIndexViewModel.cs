using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.GameViewModel
{
    public class GameIndexViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Developer> Developers { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
