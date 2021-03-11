using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSourceUser
{
    public class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        private DbSet<UserRoleService> repo => context.Set<UserRoleService>();

        public UserRoleService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
