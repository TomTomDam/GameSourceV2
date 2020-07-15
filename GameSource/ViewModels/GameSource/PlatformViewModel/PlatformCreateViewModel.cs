using GameSource.Models.GameSource;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSource.PlatformViewModel
{
    public class PlatformCreateViewModel
    {
        public Platform Platform { get; set; }
        public List<SelectListItem> PlatformTypes { get; set; }
    }
}
