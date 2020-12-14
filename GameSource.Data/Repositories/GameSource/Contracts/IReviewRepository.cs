using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.Data.Repositories.GameSource.Contracts
{
    public interface IReviewRepository
    {
        public IEnumerable<Review> GetAll();
        public Review GetByID(int id);
        public void Insert(Review review);
        public void Update(Review review);
        public void Delete(int id);
    }
}
