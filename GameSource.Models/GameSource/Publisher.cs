using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameSource.Models.GameSource
{
    public class Publisher
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
