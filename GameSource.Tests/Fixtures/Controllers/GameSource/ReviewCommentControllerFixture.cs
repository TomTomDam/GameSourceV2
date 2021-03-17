using AutoFixture;
using GameSource.API.Controllers;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.GameSource
{
    public class ReviewCommentControllerFixture
    {
        public ReviewCommentController reviewCommentController;
        public Mock<IReviewCommentRepository> mockReviewCommentRepo;
        public IFixture fixture;

        public ReviewCommentControllerFixture()
        {
            mockReviewCommentRepo = new Mock<IReviewCommentRepository>();
            reviewCommentController = new ReviewCommentController(mockReviewCommentRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
