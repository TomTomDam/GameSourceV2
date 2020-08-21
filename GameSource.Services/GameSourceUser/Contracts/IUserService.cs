using GameSource.Models.GameSourceUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserService
    {
        public IEnumerable<User> GetAll();
        public User GetByID(int id);
        public User GetByUserName(string username);
        public void Insert(User user);
        public void Update(User user);
        public void Delete(int id);
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetByIDAsync(int id);
        public Task<User> GetByUserNameAsync(string username);
        public Task<User> InsertAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(int id);
    }
}
