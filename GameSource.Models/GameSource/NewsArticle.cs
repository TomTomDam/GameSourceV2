using GameSource.Models.GameSourceUser;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSource.Models.GameSource
{
    public class NewsArticle
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        //public int CategoryID { get; set; }

        //[Display(Name = "Category")]
        //public NewsArticleCategory Category { get; set; }


        [Display(Name = "Upload Image")]
        [NotMapped]
        public IFormFile CoverImage { get; set; }

        [Display(Name = "Current Cover Image")]
        public string CoverImageFilePath { get; set; }

        [ForeignKey("CreatedBy")]
        public int CreatedByID { get; set; }

        public User CreatedBy { get; set; }
    }
}
