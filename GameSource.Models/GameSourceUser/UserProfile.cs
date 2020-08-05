using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GameSource.Models.GameSourceUser
{
    public class UserProfile
    {
        [Key]
        public int ID { get; set; }

        public string Biography { get; set; }

        public User User { get; set; }
    }
}
