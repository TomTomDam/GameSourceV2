using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserStatusService : IBaseService<UserStatus>
    {
        public UserStatus GetByName(string name);
        public Task<UserStatus> GetByNameAsync(string name);
    }
}
