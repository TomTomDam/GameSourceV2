using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSourceUser
{
    public class UserProfileCommentPermissionService : BaseService<UserProfileCommentPermission>, IUserProfileCommentPermissionService
    {
        private DbSet<UserProfileCommentPermission> entity => context.Set<UserProfileCommentPermission>();

        public UserProfileCommentPermissionService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
