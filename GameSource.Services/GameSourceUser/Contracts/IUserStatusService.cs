using GameSource.Models.GameSourceUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserStatusService
    {
        public IEnumerable<UserStatus> GetAll();
        public UserStatus GetByID(int id);
        public void Insert(UserStatus userStatus);
        public void Update(UserStatus userStatus);
        public void Delete(int id);
    }
}
