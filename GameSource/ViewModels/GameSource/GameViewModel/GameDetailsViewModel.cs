using GameSource.Models.GameSource;
using GameSource.Models.GameSourceUser;

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
        public Review Review { get; set; }
        public User SignedInUser { get; set; }
    }
}
