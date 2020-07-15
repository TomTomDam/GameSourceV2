using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Data.Repositories.GameSourceUser
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private GameSourceUser_DBContext context;
        private DbSet<User> entity;

        public UserRepository(GameSourceUser_DBContext context) : base(context)
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
            return entity.Find(id);
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
            var user = GetByID(id);
            entity.Remove(user);
            context.SaveChanges();
        }
    }
}
