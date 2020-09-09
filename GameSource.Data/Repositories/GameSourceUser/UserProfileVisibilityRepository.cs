using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSourceUser
{
    public class UserProfileVisibilityRepository : BaseRepository<UserProfileVisibility>, IUserProfileVisibilityRepository
    {
        private GameSource_DBContext context;
        private DbSet<UserProfileVisibility> entity;

        public UserProfileVisibilityRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<UserProfileVisibility>();
        }

        public IEnumerable<UserProfileVisibility> GetAll()
        {
            return entity.ToList();
        }

        public UserProfileVisibility GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(UserProfileVisibility userProfileVisibility)
        {
            entity.Add(userProfileVisibility);
            context.SaveChanges();
        }

        public void Update(UserProfileVisibility userProfileVisibility)
        {
            entity.Update(userProfileVisibility);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var userProfileVisibility = GetByID(id);
            entity.Remove(userProfileVisibility);
            context.SaveChanges();
        }

        public async Task<IEnumerable<UserProfileVisibility>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<UserProfileVisibility> GetByIDAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task<UserProfileVisibility> InsertAsync(UserProfileVisibility userProfileVisibility)
        {
            await entity.AddAsync(userProfileVisibility);
            await context.SaveChangesAsync();
            return userProfileVisibility;
        }

        public async Task UpdateAsync(UserProfileVisibility userProfileVisibility)
        {
            entity.Update(userProfileVisibility);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userProfileVisibility = await GetByIDAsync(id);
            entity.Remove(userProfileVisibility);
            await context.SaveChangesAsync();
        }
    }
}
