using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserProfileVisibilityService : IBaseService<UserProfileVisibility>
    {
        public UserProfileVisibility GetByUserProfileID(int id);
        public Task<UserProfileVisibility> GetByUserProfileIDAsync(int id);
    }
}
