using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameSource.Models
{
    public class Platform
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public PlatformType PlatformType { get; set; }
    }
}
