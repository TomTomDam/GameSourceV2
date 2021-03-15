using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Infrastructure.Repositories.GameSourceUser.Contracts
{
    public interface IUserProfileRepository : IBaseRepository<UserProfile>
    {
        UserProfile GetByUserID(int id);
        Task<UserProfile> GetByUserIDAsync(int id);
    }
}
