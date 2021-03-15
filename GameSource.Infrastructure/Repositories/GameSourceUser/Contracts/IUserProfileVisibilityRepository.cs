using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSourceUser.Contracts
{
    public interface IUserProfileVisibilityRepository : IBaseRepository<UserProfileVisibility>
    {
        public UserProfileVisibility GetByUserProfileID(int id);
        public Task<UserProfileVisibility> GetByUserProfileIDAsync(int id);
    }
}
