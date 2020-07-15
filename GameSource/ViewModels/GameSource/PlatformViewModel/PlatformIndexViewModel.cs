using GameSource.Models.GameSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSource.PlatformViewModel
{
    public class PlatformIndexViewModel
    {
        public IEnumerable<Platform> Platforms { get; set; }
        public IEnumerable<PlatformType> PlatformTypes { get; set; }
    }
}
