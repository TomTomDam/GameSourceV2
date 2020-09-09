using GameSource.Models.GameSourceUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserProfileVisibilityService
    {
        public IEnumerable<UserProfileVisibility> GetAll();
        public UserProfileVisibility GetByID(int id);
        public UserProfileVisibility GetByUserProfileID(int id);
        public void Insert(UserProfileVisibility userProfileVisibility);
        public void Update(UserProfileVisibility userProfileVisibility);
        public void Delete(int id);
        public Task<IEnumerable<UserProfileVisibility>> GetAllAsync();
        public Task<UserProfileVisibility> GetByIDAsync(int id);
        public Task<UserProfileVisibility> GetByUserProfileIDAsync(int id);
        public Task InsertAsync(UserProfileVisibility userProfileVisibility);
        public Task UpdateAsync(UserProfileVisibility userProfileVisibility);
        public Task DeleteAsync(int id);
    }
}
