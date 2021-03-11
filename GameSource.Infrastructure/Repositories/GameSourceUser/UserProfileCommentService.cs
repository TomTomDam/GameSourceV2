using GameSource.Infrastructure;
using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GameSource.Infrastructure.Repositories.GameSourceUser
{
    public class UserProfileCommentRepository : BaseRepository<UserProfileComment>, IUserProfileCommentRepository
    {
        private DbSet<UserProfileComment> repo => context.Set<UserProfileComment>();

        public UserProfileCommentRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
