using GameSource.Models.GameSourceUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Data.Repositories.GameSourceUser.Contracts
{
    public interface IUserStatusRepository
    {
        public IEnumerable<UserStatus> GetAll();
        public UserStatus GetByID(int id);
        public void Insert(UserStatus userStatus);
        public void Update(UserStatus userStatus);
        public void Delete(int id);
    }
}
