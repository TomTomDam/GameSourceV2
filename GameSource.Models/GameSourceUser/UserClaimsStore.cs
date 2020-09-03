using System.Collections.Generic;
using System.Security.Claims;

namespace GameSource.Models.GameSourceUser
{
    public static class UserClaimsStore
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim("Create Role", "Create Role"),
            new Claim("Edit Role", "Edit Role"),
            new Claim("Delete Role", "Delete Role")
        };
    }
}
