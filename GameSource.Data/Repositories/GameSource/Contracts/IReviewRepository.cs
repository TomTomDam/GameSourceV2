using GameSource.Models.GameSource;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface IReviewRepository
    {
        public IEnumerable<Review> GetAll();
        public Review GetByID(int id);
        public void Insert(Review review);
        public void Update(Review review);
        public void Delete(int id);
        public Task<IEnumerable<Review>> GetAllAsync();
        public Task<Review> GetByIDAsync(int id);
        public Task InsertAsync(Review review);
        public Task UpdateAsync(Review review);
        public Task DeleteAsync(int id);
    }
}
