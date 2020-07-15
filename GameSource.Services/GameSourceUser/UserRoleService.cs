using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser
{
    public class UserRoleService : BaseService<UserStatus>, IUserRoleService
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

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<UserRole> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task InsertAsync(UserRole userRole)
        {
            await repo.InsertAsync(userRole);
        }

        public async Task UpdateAsync(UserRole userRole)
        {
            await repo.UpdateAsync(userRole);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
