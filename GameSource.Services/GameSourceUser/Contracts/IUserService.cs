using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource.Contracts;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserService : IBaseService<User>
    {
        public User GetByUserName(string username);
        public Task<User> GetByUserNameAsync(string username);
    }
}
