using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser
{
    public class UserProfileCommentPermissionService : IUserProfileCommentPermissionService
    {
        private IUserProfileCommentPermissionRepository repo;

        public UserProfileCommentPermissionService(IUserProfileCommentPermissionRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<UserProfileCommentPermission> GetAll()
        {
            return repo.GetAll();
        }

        public UserProfileCommentPermission GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(UserProfileCommentPermission userProfileCommentPermission)
        {
            repo.Insert(userProfileCommentPermission);
        }

        public void Update(UserProfileCommentPermission userProfileCommentPermission)
        {
            repo.Update(userProfileCommentPermission);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public async Task<IEnumerable<UserProfileCommentPermission>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<UserProfileCommentPermission> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task InsertAsync(UserProfileCommentPermission userProfileCommentPermission)
        {
            await repo.InsertAsync(userProfileCommentPermission);
        }

        public async Task UpdateAsync(UserProfileCommentPermission userProfileCommentPermission)
        {
            await repo.UpdateAsync(userProfileCommentPermission);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
