using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameSource.Models
{
    public class Platform
    {
        [Key]
        public int Platform_ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
