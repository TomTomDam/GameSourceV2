using GameSource.Models.GameSourceUser;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.ViewModels.GameSourceUser.AccountViewModel
{
    public class AccountIndexViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
        public IEnumerable<UserStatus> UserStatuses { get; set; }
    }
}
