using GameSource.Models.GameSource;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GameSource.Models.GameSourceUser
{
    public class User : IdentityUser<Guid>
    {
        public User()
        {

        }

        [StringLength(40)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(40)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public int? Age { get; set; }

        public string Location { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        public int UserStatusID { get; set; }

        public Guid UserRoleID { get; set; }

        [JsonIgnore]
        [Display(Name = "User Status")]
        public UserStatus UserStatus { get; set; }

        [JsonIgnore]
        [Display(Name = "User Role")]
        public Role Role { get; set; }

        [JsonIgnore]
        public UserProfile UserProfile { get; set; }

        public ICollection<Review> ReviewsCreated { get; set; }

        public ICollection<ReviewComment> ReviewCommentsCreated { get; set; }

        public ICollection<UserProfileComment> UserProfileCommentsCreated { get; set; }

        public ICollection<NewsArticle> NewsArticlesCreated { get; set; }

        //public ICollection<EventsLog> EventLogsCreated { get; set; }
    }
}
