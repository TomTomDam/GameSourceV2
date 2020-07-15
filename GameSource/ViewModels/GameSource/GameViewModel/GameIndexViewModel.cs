using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSource.GameViewModel
{
    public class GameIndexViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Developer> Developers { get; set; }
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }
    }
}
