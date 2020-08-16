using GameSource.Models.GameSourceUser;
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

        [ForeignKey("AuthoredBy")]
        public int AuthoredByID { get; set; }

        public User AuthoredBy { get; set; }
    }
}
