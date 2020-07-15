using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSource.PublisherViewModel
{
    public class PublisherIndexViewModel
    {
        public IEnumerable<Publisher> Publishers { get; set; }
    }
}
