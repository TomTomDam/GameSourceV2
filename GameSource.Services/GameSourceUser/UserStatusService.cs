using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser
{
    public class UserStatusService : BaseService<UserStatus>, IUserStatusService
    {
        private DbSet<UserStatus> repo => context.Set<UserStatus>();

        public UserStatusService(GameSource_DBContext context) : base(context)
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
