using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSourceUser
{
    public class UserProfileCommentPermissionRepository : BaseRepository<UserProfileCommentPermission>, IUserProfileCommentPermissionRepository
    {
        private GameSource_DBContext context;
        private DbSet<UserProfileCommentPermission> entity;

        public UserProfileCommentPermissionRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<UserProfileCommentPermission>();
        }

        public IEnumerable<UserProfileCommentPermission> GetAll()
        {
            return entity.ToList();
        }

        public UserProfileCommentPermission GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(UserProfileCommentPermission userProfileCommentPermission)
        {
            entity.Add(userProfileCommentPermission);
            context.SaveChanges();
        }

        public void Update(UserProfileCommentPermission userProfileCommentPermission)
        {
            entity.Update(userProfileCommentPermission);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var userProfileCommentPermission = GetByID(id);
            entity.Remove(userProfileCommentPermission);
            context.SaveChanges();
        }

        public async Task<IEnumerable<UserProfileCommentPermission>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<UserProfileCommentPermission> GetByIDAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task<UserProfileCommentPermission> InsertAsync(UserProfileCommentPermission userProfileCommentPermission)
        {
            await entity.AddAsync(userProfileCommentPermission);
            await context.SaveChangesAsync();
            return userProfileCommentPermission;
        }

        public async Task UpdateAsync(UserProfileCommentPermission userProfileCommentPermission)
        {
            entity.Update(userProfileCommentPermission);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userProfileCommentPermission = await GetByIDAsync(id);
            entity.Remove(userProfileCommentPermission);
            await context.SaveChangesAsync();
        }
    }
}
