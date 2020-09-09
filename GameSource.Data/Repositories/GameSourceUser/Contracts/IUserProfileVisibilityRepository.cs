using GameSource.Models.GameSourceUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSourceUser.Contracts
{
    public interface IUserProfileVisibilityRepository
    {
        public IEnumerable<UserProfileVisibility> GetAll();
        public UserProfileVisibility GetByID(int id);
        public void Insert(UserProfileVisibility userProfileVisibility);
        public void Update(UserProfileVisibility userProfileVisibility);
        public void Delete(int id);
        public Task<IEnumerable<UserProfileVisibility>> GetAllAsync();
        public Task<UserProfileVisibility> GetByIDAsync(int id);
        public Task<UserProfileVisibility> InsertAsync(UserProfileVisibility userProfileVisibility);
        public Task UpdateAsync(UserProfileVisibility userProfileVisibility);
        public Task DeleteAsync(int id);
    }
}
