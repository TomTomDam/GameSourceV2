using GameSource.Data.Repositories.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSourceUser
{
    public class ClaimsTransformer : IClaimsTransformer
    {
        private readonly GameSource_DBContext context;

        public ClaimsTransformer(GameSource_DBContext context)
        {
            this.context = context;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var existingClaimsIdentity = (ClaimsIdentity)principal.Identity;
            var currentUserName = existingClaimsIdentity.Name;

            // Initialize a new list of claims for the new identity
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, currentUserName),
                // Potentially add more from the existing claims here
            };

            // Find the user in the DB
            // Add as many role claims as they have roles in the DB
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(currentUserName, StringComparison.CurrentCultureIgnoreCase));
            if (user != null)
            {
                var rolesNames = from ur in context.UserRoles.Where(p => p.UserId == user.Id)
                                 from r in context.Roles
                                 where ur.RoleId == r.Id
                                 select r.Name;

                claims.AddRange(rolesNames.Select(x => new Claim(ClaimTypes.Role, x)));
            }

            // Build and return the new principal
            var newClaimsIdentity = new ClaimsIdentity(claims, existingClaimsIdentity.AuthenticationType);
            return new ClaimsPrincipal(newClaimsIdentity);
        }
    }
}
