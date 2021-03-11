using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSourceUser
{
    public class UserStatusRepository : BaseRepository<UserStatus>, IUserStatusRepository
    {
        private DbSet<UserStatus> repo => context.Set<UserStatus>();

        public UserStatusRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }

        public UserStatus GetByName(string name)
        {
            return repo.Where(x => x.Name == name).FirstOrDefault();
        }

        public async Task<UserStatus> GetByNameAsync(string name)
        {
            return await repo.Where(x => x.Name == name).FirstOrDefaultAsync();
        }
    }
}
