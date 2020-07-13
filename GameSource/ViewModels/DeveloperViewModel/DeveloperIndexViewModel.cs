using GameSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.DeveloperViewModel
{
    public class DeveloperIndexViewModel
    {
        public IEnumerable<Developer> Developers { get; set; }
    }
}
