using GameSource.Models.GameSourceUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSourceUser.Contracts
{
    public interface IUserStatusRepository
    {
        public IEnumerable<UserStatus> GetAll();
        public UserStatus GetByID(int id);
        public UserStatus GetByName(string name);
        public void Insert(UserStatus userStatus);
        public void Update(UserStatus userStatus);
        public void Delete(int id);
        public Task<IEnumerable<UserStatus>> GetAllAsync();
        public Task<UserStatus> GetByIDAsync(int id);
        public Task<UserStatus> GetByNameAsync(string name);
        public Task InsertAsync(UserStatus userStatus);
        public Task UpdateAsync(UserStatus userStatus);
        public Task DeleteAsync(int id);
    }
}
