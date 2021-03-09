using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSource.Models
{
    public class Game
    {
        public Game()
        {

        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "The {0} cannot exceed {1} characters.")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Upload Image")]
        [NotMapped]
        public IFormFile CoverImage { get; set; }

        [Display(Name = "Current Cover Image")]
        public string CoverImageFilePath { get; set; }
    }
}
