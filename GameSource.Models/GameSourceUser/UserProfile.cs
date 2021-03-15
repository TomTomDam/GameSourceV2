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

        [StringLength(40)]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        public string Biography { get; set; }

        //[Display(Name = "Upload Image")]
        //[NotMapped]
        //public IFormFile AvatarImage { get; set; }

        [Display(Name = "Current Avatar Image")]
        public string AvatarFilePath { get; set; }

        //[Display(Name = "Upload Image")]
        //[NotMapped]
        //public IFormFile ProfileBackgroundImage { get; set; }

        [Display(Name = "Current Profile Background Image")]
        public string ProfileBackgroundImageFilePath { get; set; }

        public int UserID { get; set; }

        [Display(Name = "Profile Visibility")]
        public int? UserProfileVisibilityID { get; set; }

        [Display(Name = "Comment Visibility")]
        public int? UserProfileCommentPermissionID { get; set; }

        public User User { get; set; }

        public UserProfileVisibility UserProfileVisibility { get; set; }

        public UserProfileCommentPermission UserProfileCommentPermission { get; set; }

        public ICollection<UserProfileComment> Comments { get; set; }
    }
}
