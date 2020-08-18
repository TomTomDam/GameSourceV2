using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSource.Models.GameSourceUser
{
    public class UserProfile
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        public string Biography { get; set; }

        public string AvatarFilePath { get; set; }

        [Display(Name = "Upload Image")]
        [NotMapped]
        public IFormFile AvatarImage { get; set; }

        public int UserID { get; set; }

        public int UserProfileVisibilityID { get; set; }

        public int UserProfileCommentPermissionID { get; set; }

        public User User { get; set; }

        public UserProfileVisibility UserProfileVisibility { get; set; }

        public UserProfileCommentPermission UserProfileCommentPermission { get; set; }

        public ICollection<UserProfileComment> Comments { get; set; }
    }
}
