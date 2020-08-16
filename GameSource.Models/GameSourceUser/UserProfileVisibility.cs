using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameSource.Models.GameSourceUser
{
    public class UserProfileVisibility
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<UserProfile> UserProfile { get; set; }
    }
}
