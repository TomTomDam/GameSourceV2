using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSource.GenreViewModel
{
    public class GenreIndexViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
}
