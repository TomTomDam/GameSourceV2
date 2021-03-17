using AutoFixture;
using GameSource.API.Controllers;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.GameSource
{
    public class ReviewControllerFixture
    {
        public ReviewController reviewController;
        public Mock<IReviewRepository> mockReviewRepo;
        public IFixture fixture;

        public ReviewControllerFixture()
        {
            mockReviewRepo = new Mock<IReviewRepository>();
            reviewController = new ReviewController(mockReviewRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
