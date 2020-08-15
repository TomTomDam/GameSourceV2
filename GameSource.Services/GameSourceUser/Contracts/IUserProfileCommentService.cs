using GameSource.Models.GameSourceUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserProfileCommentService
    {
        public IEnumerable<UserProfileComment> GetAll();
        public UserProfileComment GetByID(int id);
        public void Insert(UserProfileComment userProfile);
        public void Update(UserProfileComment userProfile);
        public void Delete(int id);
        public Task<IEnumerable<UserProfileComment>> GetAllAsync();
        public Task<UserProfileComment> GetByIDAsync(int id);
        public Task InsertAsync(UserProfileComment userProfile);
        public Task UpdateAsync(UserProfileComment userProfile);
        public Task DeleteAsync(int id);
    }
}
