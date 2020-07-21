using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSource.GenreViewModel
{
    public static class GenreMappingExtensions
    {
        public static Genre ToViewModel(this Genre src)
        {
            return new Genre
            {
                ID = src.ID,
                Name = src.Name
            };
        }
    }
}
