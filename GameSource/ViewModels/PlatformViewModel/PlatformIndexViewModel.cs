using GameSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.PlatformViewModel
{
    public class PlatformIndexViewModel
    {
        public IEnumerable<Platform> Platforms { get; set; }
        public IEnumerable<PlatformType> PlatformTypes { get; set; }
    }
}
