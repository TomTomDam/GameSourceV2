using GameSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.PublisherViewModel
{
    public class PublisherIndexViewModel
    {
        public IEnumerable<Publisher> Publishers { get; set; }
    }
}
