using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.Genre
{
    public class GenreViewModel
    {
        public int Genre_ID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public SelectList Genres { get; set; }
    }
}
