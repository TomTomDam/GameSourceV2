using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSourceUser
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private GameSource_DBContext context;
        private DbSet<User> entity;

        public UserRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<User>();
        }

        public IEnumerable<User> GetAll()
        {
            return entity.ToList();
        }

        public User GetByID(int id)
        {
            return entity
                .Include(x => x.UserProfile)
                .Include(x => x.UserProfileCommentsCreated)
                .Include(x => x.UserRole)
                .Include(x => x.UserStatus)
                .Include(x => x.NewsArticlesCreated)
                .SingleOrDefault(x => x.Id == id);
        }

        public void Insert(User user)
        {
            entity.Add(user);
            context.SaveChanges();
        }

        public void Update(User user)
        {
            entity.Update(user);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            User user = GetByID(id);
            entity.Remove(user);
            context.SaveChanges();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<User> GetByIDAsync(int id)
        {
            return await entity
                .Include(x => x.UserProfile)
                .Include(x => x.UserProfileCommentsCreated)
                .Include(x => x.UserRole)
                .Include(x => x.UserStatus)
                .Include(x => x.NewsArticlesCreated)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> InsertAsync(User user)
        {
            await entity.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            entity.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            User user = await GetByIDAsync(id);
            entity.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}
