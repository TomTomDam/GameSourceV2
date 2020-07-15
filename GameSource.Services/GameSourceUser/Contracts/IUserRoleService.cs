using GameSource.Models.GameSourceUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserRoleService
    {
        public IEnumerable<UserRole> GetAll();
        public UserRole GetByID(int id);
        public void Insert(UserRole userRole);
        public void Update(UserRole userRole);
        public void Delete(int id);
        public Task<IEnumerable<UserRole>> GetAllAsync();
        public Task<UserRole> GetByIDAsync(int id);
        public Task InsertAsync(UserRole userRole);
        public Task UpdateAsync(UserRole userRole);
        public Task DeleteAsync(int id);
    }
}
