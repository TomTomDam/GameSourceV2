using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameSource.Models.GameSourceUser
{
    public class UserProfile
    {
        [Key]
        public int ID { get; set; }

        public string Biography { get; set; }

        public int Visibility { get; set; }

        public int CommentPermission { get; set; }

        public int UserID { get; set; }

        public User User { get; set; }

        public ICollection<UserProfileComment> Comments { get; set; }
    }
}
