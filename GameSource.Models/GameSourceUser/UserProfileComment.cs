using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GameSource.Models.GameSourceUser
{
    public class UserProfileComment
    {
        [Key]
        public int ID { get; set; }

        public string Body { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("CreatedBy")]
        public Guid CreatedByID { get; set; }

        public int UserProfileID { get; set; }

        [JsonIgnore]
        public User CreatedBy { get; set; }

        [JsonIgnore]
        public UserProfile UserProfile { get; set; }
    }
}
