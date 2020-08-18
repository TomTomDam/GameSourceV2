using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSourceUser
{
    public class UserStatusRepository : BaseRepository<UserStatus>, IUserStatusRepository
    {
        private GameSource_DBContext context;
        private DbSet<UserStatus> entity;

        public UserStatusRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<UserStatus>();
        }

        public IEnumerable<UserStatus> GetAll()
        {
            return entity.ToList();
        }

        public UserStatus GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(UserStatus userStatus)
        {
            entity.Add(userStatus);
            context.SaveChanges();
        }

        public void Update(UserStatus userStatus)
        {
            entity.Update(userStatus);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var userStatus = GetByID(id);
            entity.Remove(userStatus);
            context.SaveChanges();
        }

        public async Task<IEnumerable<UserStatus>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<UserStatus> GetByIDAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task InsertAsync(UserStatus userStatus)
        {
            await entity.AddAsync(userStatus);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserStatus userStatus)
        {
            entity.Update(userStatus);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userStatus = await GetByIDAsync(id);
            entity.Remove(userStatus);
            await context.SaveChangesAsync();
        }
    }
}
