using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser
{
    public class UserProfileCommentService : BaseService<UserProfileComment>, IUserProfileCommentService
    {
        private IUserProfileCommentRepository repo;

        public UserProfileCommentService(IUserProfileCommentRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<UserProfileComment> GetAll()
        {
            return repo.GetAll();
        }

        public UserProfileComment GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(UserProfileComment userProfile)
        {
            repo.Insert(userProfile);
        }

        public void Update(UserProfileComment userProfile)
        {
            repo.Update(userProfile);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public async Task<IEnumerable<UserProfileComment>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<UserProfileComment> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task InsertAsync(UserProfileComment userProfile)
        {
            await repo.InsertAsync(userProfile);
        }

        public async Task UpdateAsync(UserProfileComment userProfile)
        {
            await repo.UpdateAsync(userProfile);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
