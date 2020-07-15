using GameSource.Models.GameSourceUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserRoleService
    {
        public IEnumerable<UserRole> GetAll();
        public UserRole GetByID(int id);
        public void Insert(UserRole userRole);
        public void Update(UserRole userRole);
        public void Delete(int id);
    }
}
