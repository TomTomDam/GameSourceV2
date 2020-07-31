using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSourceUser
{
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
        private GameSourceUser_DBContext context;
        private DbSet<UserProfile> entity;

        public UserProfileRepository(GameSourceUser_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<UserProfile>();
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return entity.ToList();
        }

        public UserProfile GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(UserProfile userProfile)
        {
            entity.Add(userProfile);
            context.SaveChanges();
        }

        public void Update(UserProfile userProfile)
        {
            entity.Update(userProfile);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var userProfile = GetByID(id);
            entity.Remove(userProfile);
            context.SaveChanges();
        }

        public async Task<IEnumerable<UserProfile>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<UserProfile> GetByIDAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task<UserProfile> InsertAsync(UserProfile userProfile)
        {
            await entity.AddAsync(userProfile);
            await context.SaveChangesAsync();
            return userProfile;
        }

        public async Task UpdateAsync(UserProfile userProfile)
        {
            entity.Update(userProfile);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userProfile = await GetByIDAsync(id);
            entity.Remove(userProfile);
            await context.SaveChangesAsync();
        }
    }
}
