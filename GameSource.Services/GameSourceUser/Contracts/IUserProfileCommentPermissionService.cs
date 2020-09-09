using GameSource.Models.GameSourceUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser.Contracts
{
    public interface IUserProfileCommentPermissionService
    {
        public IEnumerable<UserProfileCommentPermission> GetAll();
        public UserProfileCommentPermission GetByID(int id);
        public void Insert(UserProfileCommentPermission userProfileCommentPermission);
        public void Update(UserProfileCommentPermission userProfileCommentPermission);
        public void Delete(int id);
        public Task<IEnumerable<UserProfileCommentPermission>> GetAllAsync();
        public Task<UserProfileCommentPermission> GetByIDAsync(int id);
        public Task InsertAsync(UserProfileCommentPermission userProfileCommentPermission);
        public Task UpdateAsync(UserProfileCommentPermission userProfileCommentPermission);
        public Task DeleteAsync(int id);
    }
}
