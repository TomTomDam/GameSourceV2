using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource
{
    public class ReviewCommentService : IReviewCommentService
    {
        public IReviewCommentRepository repo;

        public ReviewCommentService(IReviewCommentRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<ReviewComment> GetAll()
        {
            return repo.GetAll();
        }

        public ReviewComment GetByID(int id)
        {
            return repo.GetByID(id);
        }

        public void Insert(ReviewComment reviewComment)
        {
            repo.Insert(reviewComment);
        }

        public void Update(ReviewComment reviewComment)
        {
            repo.Update(reviewComment);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public async Task<IEnumerable<ReviewComment>> GetAllAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<ReviewComment> GetByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task InsertAsync(ReviewComment reviewComment)
        {
            await repo.InsertAsync(reviewComment);
        }

        public async Task UpdateAsync(ReviewComment reviewComment)
        {
            await repo.UpdateAsync(reviewComment);
        }

        public async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
        }
    }
}
