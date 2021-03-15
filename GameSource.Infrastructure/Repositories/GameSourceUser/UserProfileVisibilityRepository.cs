using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSourceUser
{
    public class UserProfileVisibilityRepository : BaseRepository<UserProfileVisibility>, IUserProfileVisibilityRepository
    {
        private DbSet<UserProfileVisibility> repo => context.Set<UserProfileVisibility>();
        private DbSet<UserProfile> userProfileRepo => context.Set<UserProfile>();

        public UserProfileVisibilityRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }

        public UserProfileVisibility GetByUserProfileID(int id)
        {
            UserProfile userProfile = userProfileRepo.Find(id);
            return repo.Find(userProfile.UserProfileVisibility.ID);
        }

        public async Task<UserProfileVisibility> GetByUserProfileIDAsync(int id)
        {
            UserProfile userProfile = await userProfileRepo.FindAsync(id);
            return await repo.FindAsync(userProfile.UserProfileVisibility.ID);
        }
    }
}
