using GameSource.Models.GameSourceUser;
using System;
using System.ComponentModel.DataAnnotations;

namespace GameSource.Models.GameSource
{
    public class NewsArticle
    {
        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int CreatedByID { get; set; }

        public User CreatedBy { get; set; }
    }
}
