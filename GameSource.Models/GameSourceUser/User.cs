using GameSource.Models.GameSource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSource.Models.GameSourceUser
{
    public class User : IdentityUser<int>
    {
        [StringLength(20)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        public int? Age { get; set; }

        public string Location { get; set; }

        public DateTime DateCreated { get; set; }

        public string AvatarFilePath { get; set; }

        [Display(Name = "Upload Image")]
        [NotMapped]
        public IFormFile AvatarImage { get; set; }

        public string Description { get; set; }

        public int UserStatusID { get; set; }

        public int UserRoleID { get; set; }

        public int UserProfileID { get; set; }

        public UserStatus UserStatus { get; set; }

        public UserRole UserRole { get; set; }

        public UserProfile UserProfile { get; set; }

        public ICollection<UserProfileComment> UserProfileCommentsCreated { get; set; }

        public ICollection<NewsArticle> NewsArticlesCreated { get; set; }
    }
}
