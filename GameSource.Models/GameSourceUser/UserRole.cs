using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Models
{
    public class UserRole : IdentityRole<int>
    {
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
