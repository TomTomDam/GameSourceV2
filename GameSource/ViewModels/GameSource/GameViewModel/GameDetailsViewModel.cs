using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSource.GameViewModel
{
    public class GameDetailsViewModel
    {
        public Game Game { get; set; }
        public int GenreID { get; set; }
        public int DeveloperID { get; set; }
        public int PublisherID { get; set; }
        public int PlatformID { get; set; }
        public Genre Genre { get; set; }
        public Developer Developer { get; set; }
        public Publisher Publisher { get; set; }
        public Platform Platform { get; set; }
    }
}
