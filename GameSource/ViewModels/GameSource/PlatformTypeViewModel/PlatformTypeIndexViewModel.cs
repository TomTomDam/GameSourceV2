using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.PlatformTypeViewModel
{
    public class PlatformTypeIndexViewModel
    {
        public PlatformTypeIndexViewModel()
        {
            PlatformTypes = new List<PlatformType>();
        }

        public IEnumerable<PlatformType> PlatformTypes { get; set; }
    }
}
