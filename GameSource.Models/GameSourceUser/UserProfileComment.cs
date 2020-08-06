using System;
using System.ComponentModel.DataAnnotations;

namespace GameSource.Models.GameSourceUser
{
    public class UserProfileComment
    {
        [Key]
        public int ID { get; set; }

        public string Body { get; set; }

        public DateTime DateCreated { get; set; }

        public int CreatedByID { get; set; }

        public int UserProfileID { get; set; }

        public User CreatedBy { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
