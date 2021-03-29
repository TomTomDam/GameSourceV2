using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace GameSource.Infrastructure.Repositories.GameSourceUser
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private DbSet<RoleRepository> repo => context.Set<RoleRepository>();

        public RoleRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Role> GetByIDAsync(Guid guid)
        {
            var item = await entity.FindAsync(guid);
            return item;
        }
    }
}
