using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameSource.Models.GameSource
{
    public class Developer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
