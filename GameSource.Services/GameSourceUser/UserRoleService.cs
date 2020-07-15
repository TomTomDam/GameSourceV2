using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services.GameSourceUser
{
    public class UserRoleService : IUserRoleService
    {
        public IUserRoleRepository repo;

        public UserRoleService(IUserRoleRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<UserRole> GetAll()
        {
            return repo.GetAll();
        }

        public UserRole GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(UserRole userRole)
        {
            repo.Insert(userRole);
        }

        public void Update(UserRole userRole)
        {
            repo.Update(userRole);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }
    }
}
