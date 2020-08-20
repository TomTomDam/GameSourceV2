using GameSource.Models.GameSourceUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserProfileService
    {
        public IEnumerable<UserProfile> GetAll();
        public UserProfile GetByID(int id);
        public UserProfile GetByUserID(int id);
        public void Insert(UserProfile userProfile);
        public void Update(UserProfile userProfile);
        public void Delete(int id);
        public Task<IEnumerable<UserProfile>> GetAllAsync();
        public Task<UserProfile> GetByIDAsync(int id);
        public Task<UserProfile> GetByUserIDAsync(int id);
        public Task InsertAsync(UserProfile userProfile);
        public Task UpdateAsync(UserProfile userProfile);
        public Task DeleteAsync(int id);
    }
}
