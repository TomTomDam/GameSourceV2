using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSourceUser.Contracts
{
    public interface IUserStatusRepository : IBaseRepository<UserStatus>
    {
        public UserStatus GetByName(string name);
        public Task<UserStatus> GetByNameAsync(string name);
    }
}
