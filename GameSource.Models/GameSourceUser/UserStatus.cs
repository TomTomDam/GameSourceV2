using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameSource.Models.GameSourceUser
{
    public class UserStatus
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
