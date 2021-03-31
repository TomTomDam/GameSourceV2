using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GameSource.Models.GameSource
{
    public class Platform
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string Name { get; set; }

        public int? PlatformTypeID { get; set; }

        [JsonIgnore]
        [Display(Name = "Platform Type")]
        public PlatformType PlatformType { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
