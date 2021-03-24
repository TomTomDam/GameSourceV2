using GameSource.Models.GameSourceUser;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSource.Models.GameSource
{
    public class ReviewComment
    {
        [Key]
        public int ID { get; set; }

        public string Body { get; set; }

        public DateTime DateCreated { get; set; }

        public User CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public int CreatedByID { get; set; }

        public Review Review { get; set; }

        public int ReviewID { get; set; }
    }
}
