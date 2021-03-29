using GameSource.Models.GameSourceUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSource.Models.GameSource
{
    public class Review
    {
        public Review()
        {
            ReviewComments = new List<ReviewComment>();
        }

        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public decimal Rating { get; set; }

        public int HelpfulRating { get; set; }

        public Game Game { get; set; }

        public int GameID { get; set; }

        public User CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public Guid CreatedByID { get; set; }

        public ICollection<ReviewComment> ReviewComments { get; set; }
    }
}
