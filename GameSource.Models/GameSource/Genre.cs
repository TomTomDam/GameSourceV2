using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameSource.Models.GameSource
{
    public class Genre
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
