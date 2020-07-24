using GameSource.Models.GameSourceUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSourceUser.AccountViewModel
{
    public class AccountDetailsViewModel
    {
        public User User { get; set; }
        public int UserID { get; set; }
        public int UserRoleID { get; set; }
        public int UserStatusID { get; set; }
        public UserRole UserRole { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}
