using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Services.GameSource.Contracts
{
    public interface IReviewCommentService
    {
        public IEnumerable<ReviewComment> GetAll();
        public ReviewComment GetByID(int id);
        public void Insert(ReviewComment reviewComment);
        public void Update(ReviewComment reviewComment);
        public void Delete(int id);
        public Task<IEnumerable<ReviewComment>> GetAllAsync();
        public Task<ReviewComment> GetByIDAsync(int id);
        public Task InsertAsync(ReviewComment reviewComment);
        public Task UpdateAsync(ReviewComment reviewComment);
        public Task DeleteAsync(int id);
    }
}
