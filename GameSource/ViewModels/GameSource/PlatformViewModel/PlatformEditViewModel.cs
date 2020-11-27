using GameSource.Models.GameSource;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GameSource.ViewModels.GameSource.PlatformViewModel
{
    public class PlatformEditViewModel
    {
        public PlatformEditViewModel()
        {
            PlatformTypes = new List<SelectListItem>();
        }

        public Platform Platform { get; set; }
        public int? PlatformTypeID { get; set; }
        public List<SelectListItem> PlatformTypes { get; set; }
    }
}
