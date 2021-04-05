using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameSource.Models.DTOs.GameSource
{
    public class UpdateGameDTO
    {
        public UpdateGameDTO()
        {
            Reviews = new List<ReviewDTO>();
            Platforms = new List<PlatformDTO>();
        }

        public int ID { get; set; }

        [StringLength(60, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        //[Display(Name = "Upload Image")]
        //[NotMapped]
        //public IFormFile CoverImage { get; set; }

        [Display(Name = "Current Cover Image")]
        public string CoverImageFilePath { get; set; }

        public int GenreID { get; set; }

        public int DeveloperID { get; set; }

        public int PublisherID { get; set; }

        public IEnumerable<PlatformDTO> Platforms { get; set; }

        public IEnumerable<ReviewDTO> Reviews { get; set; }
    }
}
