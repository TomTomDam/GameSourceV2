using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.Services.GameSource.Contracts
{
    public interface IReviewService
    {
        public IEnumerable<Review> GetAll();
        public Review GetByID(int id);
        public void Insert(Review review);
        public void Update(Review review);
        public void Delete(int id);
        public IEnumerable<ReviewComment> GetReviewComments(Review review);
    }
}
