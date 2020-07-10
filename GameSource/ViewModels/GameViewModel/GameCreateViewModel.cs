using GameSource.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameViewModel
{
    public class GameCreateViewModel
    {
        public Game Game { get; set; }
        public List<SelectListItem> Genres { get; set; }
        public List<SelectListItem> Developers { get; set; }
        public List<SelectListItem> Publishers { get; set; }
        public List<SelectListItem> Platforms { get; set; }
    }
}
