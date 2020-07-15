using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameSource.Models.GameSource
{
    public class PlatformType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Platform> Platforms { get; set; }
    }
}
