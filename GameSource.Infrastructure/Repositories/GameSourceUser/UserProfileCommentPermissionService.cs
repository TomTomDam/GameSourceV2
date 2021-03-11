using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSourceUser
{
    public class UserProfileCommentPermissionRepository : BaseRepository<UserProfileCommentPermission>, IUserProfileCommentPermissionRepository
    {
        private DbSet<UserProfileCommentPermission> repo => context.Set<UserProfileCommentPermission>();

        public UserProfileCommentPermissionRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
