using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GameSource.Models.GameSourceUser
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
