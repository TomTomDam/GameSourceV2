using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Services.GameSourceUser
{
    public class UserProfileCommentService : BaseService<UserProfileComment>, IUserProfileCommentService
    {
        private readonly GameSource_DBContext context;
        private DbSet<UserProfileComment> repo => context.Set<UserProfileComment>();

        public UserProfileCommentService(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
