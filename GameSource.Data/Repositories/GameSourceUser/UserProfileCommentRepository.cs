using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSourceUser
{
    public class UserProfileCommentRepository : BaseRepository<UserProfileComment>, IUserProfileCommentRepository
    {
        private GameSource_DBContext context;
        private DbSet<UserProfileComment> entity;

        public UserProfileCommentRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<UserProfileComment>();
        }

        public IEnumerable<UserProfileComment> GetAll()
        {
            return entity.ToList();
        }

        public UserProfileComment GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(UserProfileComment userProfileComment)
        {
            entity.Add(userProfileComment);
            context.SaveChanges();
        }

        public void Update(UserProfileComment userProfileComment)
        {
            entity.Update(userProfileComment);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var userProfileComment = GetByID(id);
            entity.Remove(userProfileComment);
            context.SaveChanges();
        }

        public async Task<IEnumerable<UserProfileComment>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<UserProfileComment> GetByIDAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task<UserProfileComment> InsertAsync(UserProfileComment userProfileComment)
        {
            await entity.AddAsync(userProfileComment);
            await context.SaveChangesAsync();
            return userProfileComment;
        }

        public async Task UpdateAsync(UserProfileComment userProfileComment)
        {
            entity.Update(userProfileComment);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userProfileComment = await GetByIDAsync(id);
            entity.Remove(userProfileComment);
            await context.SaveChangesAsync();
        }
    }
}
