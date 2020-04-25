using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameSource.Models
{
    public class Genre
    {
        [Key]
        public int Genre_ID { get; set; }
        public string Name { get; set; }
    }
}
