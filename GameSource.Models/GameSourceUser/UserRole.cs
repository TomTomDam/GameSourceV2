using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GameSource.Models.GameSourceUser
{
    public class UserRole : IdentityRole<int>
    {
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
