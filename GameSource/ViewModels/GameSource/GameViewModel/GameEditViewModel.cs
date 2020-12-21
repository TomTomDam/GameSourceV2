using GameSource.Models.GameSource;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.GameViewModel
{
    public class GameEditViewModel
    {
        public Game Game { get; set; }
        public int GenreID { get; set; }
        public int DeveloperID { get; set; }
        public int PublisherID { get; set; }
        public int PlatformID { get; set; }
        public List<SelectListItem> Genres { get; set; }
        public List<SelectListItem> Developers { get; set; }
        public List<SelectListItem> Publishers { get; set; }
        public List<SelectListItem> Platforms { get; set; }
    }
}
