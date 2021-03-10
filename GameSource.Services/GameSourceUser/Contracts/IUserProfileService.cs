using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserProfileService : IBaseService<UserProfile>
    {
        UserProfile GetByUserID(int id);
        Task<UserProfile> GetByUserIDAsync(int id);
    }
}
