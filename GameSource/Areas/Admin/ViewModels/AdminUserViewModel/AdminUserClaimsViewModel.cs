using GameSource.Models.GameSourceUser;
using System.Collections.Generic;

namespace GameSource.Areas.Admin.ViewModels.AdminUserViewModel
{
    public class AdminUserClaimsViewModel
    {
        public AdminUserClaimsViewModel()
        {
            Claims = new List<UserClaim>();
        }

        public int ID { get; set; }

        public List<UserClaim> Claims { get; set; }
    }
}
