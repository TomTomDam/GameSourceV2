using GameSource.Models.GameSource;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSource.Models.DTOs.GameSource
{
    public class GameDTO
    {
        public GameDTO()
        {
            Reviews = new List<ReviewDTO>();
            Platforms = new List<PlatformDTO>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        //[Display(Name = "Upload Image")]
        //[NotMapped]
        //public IFormFile CoverImage { get; set; }

        [Display(Name = "Current Cover Image")]
        public string CoverImageFilePath { get; set; }

        [Required]
        public int GenreID { get; set; }

        [Required]
        public int DeveloperID { get; set; }

        [Required]
        public int PublisherID { get; set; }

        public IEnumerable<PlatformDTO> Platforms { get; set; }

        public IEnumerable<ReviewDTO> Reviews { get; set; }
    }
}
