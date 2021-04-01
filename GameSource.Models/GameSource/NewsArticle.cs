using GameSource.Models.GameSourceUser;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace GameSource.Models.GameSource
{
    public class NewsArticle
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Date Modified")]
        public DateTime? DateModified { get; set; }

        public int? CategoryID { get; set; }

        [JsonIgnore]
        [Display(Name = "Category")]
        public NewsArticleCategory Category { get; set; }

        //[Display(Name = "Upload Image")]
        //[NotMapped]
        //public IFormFile CoverImage { get; set; }

        [Display(Name = "Current Cover Image")]
        public string CoverImageFilePath { get; set; }

        [ForeignKey("CreatedBy")]
        public Guid CreatedByID { get; set; }

        [JsonIgnore]
        [Display(Name = "Author")]
        public User CreatedBy { get; set; }
    }
}
