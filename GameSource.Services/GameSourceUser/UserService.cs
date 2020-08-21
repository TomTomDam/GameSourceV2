using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser
{
    public class UserService : BaseService<UserStatus>, IUserService
    {
        private IUserRepository repo;

        public UserService(IUserRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<User> GetAll()
        {
            return repo.GetAll();
        }

        public User GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public User GetByUserName(string username)
        {
            return repo.GetByUserName(username);
        }

        public void Insert(User user)
        {
            repo.Insert(user);
        }

        public void Update(User user)
        {
            repo.Update(user);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<User> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task<User> GetByUserNameAsync(string username)
        {
            return await repo.GetByUserNameAsync(username);
        }

        public async Task<User> InsertAsync(User user)
        {
            return await repo.InsertAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await repo.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
