using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameSource.Models
{
    public class PlatformType
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
