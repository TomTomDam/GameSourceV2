using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using System.Threading.Tasks;
using System;

namespace GameSource.Infrastructure.Repositories.GameSourceUser.Contracts
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public User GetByUserName(string username);
        public Task<User> GetByUserNameAsync(string username);
        public Task<User> GetByIDAsync(Guid guid);
    }
}
