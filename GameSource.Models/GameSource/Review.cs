using GameSource.Models.GameSourceUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSource.Models.GameSource
{
    public class Review
    {
        public Review()
        {
            ReviewComments = new List<ReviewComment>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public decimal Rating { get; set; }

        public User CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public int CreatedByID { get; set; }

        public ICollection<ReviewComment> ReviewComments { get; set; }
    }
}
