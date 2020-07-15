using GameSource.Models.GameSourceUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserService
    {
        public IEnumerable<User> GetAll();
        public User GetByID(int id);
        public void Insert(User user);
        public void Update(User user);
        public void Delete(int id);
    }
}
