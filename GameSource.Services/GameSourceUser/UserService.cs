using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser
{
    public class UserService : BaseService<User>, IUserService
    {
        private DbSet<User> repo => context.Set<User>();

        public UserService(GameSource_DBContext context) : base(context)
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
    }
}
