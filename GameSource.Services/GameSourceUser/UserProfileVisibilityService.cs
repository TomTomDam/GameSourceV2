using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser
{
    public class UserProfileVisibilityService : BaseService<UserProfileVisibility>, IUserProfileVisibilityService
    {
        private DbSet<UserProfileVisibility> repo => context.Set<UserProfileVisibility>();
        private DbSet<UserProfile> userProfileRepo => context.Set<UserProfile>();

        public UserProfileVisibilityService(GameSource_DBContext context) : base(context)
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
