using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameSource.Models
{
    public class Publisher
    {
        [Key]
        public int Publisher_ID { get; set; }
        public string Name { get; set; }
    }
}
