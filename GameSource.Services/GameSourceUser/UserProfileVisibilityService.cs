using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSourceUser
{
    public class UserProfileVisibilityService : BaseService<UserProfileVisibility>, IUserProfileVisibilityService
    {
        private IUserProfileVisibilityRepository repo;
        private IUserProfileRepository userProfileRepo;

        public UserProfileVisibilityService(IUserProfileVisibilityRepository repo, IUserProfileRepository userProfileRepo)
        {
            this.repo = repo;
            this.userProfileRepo = userProfileRepo;
        }

        public IEnumerable<UserProfileVisibility> GetAll()
        {
            return repo.GetAll();
        }

        public UserProfileVisibility GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public UserProfileVisibility GetByUserProfileID(int id)
        {
            UserProfile userProfile = userProfileRepo.GetByID(id);
            return repo.GetByID((int)userProfile.UserProfileVisibilityID);
        }

        public void Insert(UserProfileVisibility userProfileVisibility)
        {
            repo.Insert(userProfileVisibility);
        }

        public void Update(UserProfileVisibility userProfileVisibility)
        {
            repo.Update(userProfileVisibility);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public async Task<IEnumerable<UserProfileVisibility>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<UserProfileVisibility> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task<UserProfileVisibility> GetByUserProfileIDAsync(int id)
        {
            UserProfile userProfile = await userProfileRepo.GetByIDAsync(id);
            return repo.GetByID((int)userProfile.UserProfileVisibilityID);
        }

        public async Task InsertAsync(UserProfileVisibility userProfileVisibility)
        {
            await repo.InsertAsync(userProfileVisibility);
        }

        public async Task UpdateAsync(UserProfileVisibility userProfileVisibility)
        {
            await repo.UpdateAsync(userProfileVisibility);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
