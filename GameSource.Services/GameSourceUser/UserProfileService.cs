using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser
{
    public class UserProfileService : BaseService<UserProfile>, IUserProfileService
    {
        private DbSet<UserProfile> repo => context.Set<UserProfile>();
        private DbSet<User> userRepo => context.Set<User>();

        public UserProfileService(GameSource_DBContext context) : base(context)
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
