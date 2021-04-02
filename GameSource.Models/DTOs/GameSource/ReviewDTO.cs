using GameSource.Models.GameSource;
using GameSource.Models.GameSourceUser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GameSource.Models.DTOs.GameSource
{
    public class ReviewDTO
    {
        public ReviewDTO()
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

        public int GameID { get; set; }

        [JsonIgnore]
        public User CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public Guid CreatedByID { get; set; }

        public ICollection<ReviewComment> ReviewComments { get; set; }
    }
}
