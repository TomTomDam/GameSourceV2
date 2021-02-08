using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface IReviewCommentRepository
    {
        public IEnumerable<ReviewComment> GetAll();
        public ReviewComment GetByID(int id);
        public void Insert(ReviewComment review);
        public void Update(ReviewComment review);
        public void Delete(int id);
        public Task<IEnumerable<ReviewComment>> GetAllAsync();
        public Task<ReviewComment> GetByIDAsync(int id);
        public Task InsertAsync(ReviewComment review);
        public Task UpdateAsync(ReviewComment review);
        public Task DeleteAsync(int id);
    }
}
