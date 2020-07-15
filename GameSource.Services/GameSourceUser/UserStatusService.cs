using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services.GameSourceUser
{
    public class UserStatusService : IUserStatusService
    {
        private IUserStatusRepository repo;

        public UserStatusService(IUserStatusRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<UserStatus> GetAll()
        {
            return repo.GetAll();
        }

        public UserStatus GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(UserStatus userStatus)
        {
            repo.Insert(userStatus);
        }

        public void Update(UserStatus userStatus)
        {
            repo.Update(userStatus);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}
