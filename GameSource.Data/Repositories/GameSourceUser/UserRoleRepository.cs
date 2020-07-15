using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Data.Repositories.GameSourceUser
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        private GameSourceUser_DBContext context;
        private DbSet<UserRole> entity;

        public UserRoleRepository(GameSourceUser_DBContext context) : base(context)
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
    }
}
