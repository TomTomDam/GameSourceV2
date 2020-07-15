using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Data.Repositories.GameSourceUser
{
    public class UserStatusRepository : BaseRepository<UserStatus>, IUserStatusRepository
    {
        private GameSourceUser_DBContext context;
        private DbSet<UserStatus> entity;

        public UserStatusRepository(GameSourceUser_DBContext context) : base(context)
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
    }
}
