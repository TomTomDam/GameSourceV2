using System.ComponentModel.DataAnnotations;

namespace GameSource.Models.GameSourceUser
{
    public class UserProfileCommentPermission
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public int UserProfileID { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
