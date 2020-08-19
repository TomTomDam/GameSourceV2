using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser
{
    public class UserStatusService : BaseService<UserStatus>, IUserStatusService
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

        public UserStatus GetByName(string name)
        {
            return repo.GetByName(name);
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

        public async Task<IEnumerable<UserStatus>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<UserStatus> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task<UserStatus> GetByNameAsync(string name)
        {
            return await repo.GetByNameAsync(name);
        }

        public async Task InsertAsync(UserStatus userStatus)
        {
            await repo.InsertAsync(userStatus);
        }

        public async Task UpdateAsync(UserStatus userStatus)
        {
            await repo.UpdateAsync(userStatus);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
