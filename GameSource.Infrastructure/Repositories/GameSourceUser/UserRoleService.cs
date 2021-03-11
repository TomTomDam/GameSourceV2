using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSourceUser
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        private DbSet<UserRoleRepository> repo => context.Set<UserRoleRepository>();

        public UserRoleRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
