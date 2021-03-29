using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace GameSource.Infrastructure.Repositories.GameSourceUser
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private DbSet<User> repo => context.Set<User>();

        public UserRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }

        public User GetByUserName(string username)
        {
            return repo.Where(x => x.UserName == username).FirstOrDefault();
        }

        public async Task<User> GetByUserNameAsync(string username)
        {
            return await repo.Where(x => x.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<User> GetByIDAsync(Guid guid)
        {
            var item = await entity.FindAsync(guid);
            return item;
        }
    }
}
