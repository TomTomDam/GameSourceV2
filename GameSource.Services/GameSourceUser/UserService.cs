using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services.GameSourceUser
{
    public class UserService : IUserService
    {
        private IUserRepository repo;

        public UserService(IUserRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<User> GetAll()
        {
            return repo.GetAll();
        }

        public User GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(User user)
        {
            repo.Insert(user);
        }

        public void Update(User user)
        {
            repo.Update(user);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}
