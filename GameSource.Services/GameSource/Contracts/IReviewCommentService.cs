using GameSource.Models.GameSource;
using System.Collections.Generic;

namespace GameSource.Services.GameSource.Contracts
{
    public interface IReviewCommentService
    {
        public IEnumerable<ReviewComment> GetAll();
        public ReviewComment GetByID(int id);
        public void Insert(ReviewComment reviewComment);
        public void Update(ReviewComment reviewComment);
        public void Delete(int id);
    }
}
