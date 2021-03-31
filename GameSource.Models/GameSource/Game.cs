using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GameSource.Models.GameSource
{
    public class Game
    {
        public Game()
        {
            Reviews = new List<Review>();
        }

        [Key]
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

        [Required]
        public int PlatformID { get; set; }

        [JsonIgnore]
        public Genre Genre { get; set; }

        [JsonIgnore]
        public Developer Developer { get; set; }

        [JsonIgnore]
        public Publisher Publisher { get; set; }

        [JsonIgnore]
        public Platform Platform { get; set; }

        public IEnumerable<Review> Reviews { get; set; }
    }
}
