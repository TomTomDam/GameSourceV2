using GameSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GenreViewModel
{
    public class GenreIndexViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
}
