using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSourceUser
{
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
        private DbSet<UserProfile> repo => context.Set<UserProfile>();
        private DbSet<User> userRepo => context.Set<User>();

        public UserProfileRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }

        public UserProfile GetByUserID(int id)
        {
            User user = userRepo.Find(id);
            return repo.Find(user.UserProfile.ID);
        }

        public async Task<UserProfile> GetByUserIDAsync(int id)
        {
            User user = await userRepo.FindAsync(id);
            return await repo.FindAsync(user.UserProfile.ID);
        }
    }
}
