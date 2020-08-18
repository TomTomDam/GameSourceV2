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
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        private GameSource_DBContext context;
        private DbSet<UserRole> entity;

        public UserRoleRepository(GameSource_DBContext context) : base(context)
        {
            this.context = context;
            entity = context.Set<UserRole>();
        }

        public IEnumerable<UserRole> GetAll()
        {
            return entity.ToList();
        }

        public UserRole GetByID(int id)
        {
            return entity.Find(id);
        }

        public void Insert(UserRole userRole)
        {
            entity.Add(userRole);
            context.SaveChanges();
        }

        public void Update(UserRole userRole)
        {
            entity.Update(userRole);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var userRole = GetByID(id);
            entity.Remove(userRole);
            context.SaveChanges();
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await entity.ToListAsync();
        }

        public async Task<UserRole> GetByIDAsync(int id)
        {
            return await entity.FindAsync(id);
        }

        public async Task InsertAsync(UserRole userRole)
        {
            await entity.AddAsync(userRole);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserRole userRole)
        {
            entity.Update(userRole);
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
