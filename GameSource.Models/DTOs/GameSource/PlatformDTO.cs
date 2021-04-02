using GameSource.Models.GameSource;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameSource.Models.DTOs.GameSource
{
    public class PlatformDTO
    {
        public int ID { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string Name { get; set; }

        public int? PlatformTypeID { get; set; }

        [JsonIgnore]
        public ICollection<Game> Games { get; set; }
    }
}
