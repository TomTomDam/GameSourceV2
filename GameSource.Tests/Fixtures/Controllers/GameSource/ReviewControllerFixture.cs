using AutoFixture;
using GameSource.API;
using GameSource.API.Controllers;
using GameSource.API.Providers;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.GameSource
{
    public class ReviewControllerFixture
    {
        public ReviewController reviewController;
        public DateTimeProvider dateTimeProvider;
        public Mock<IReviewRepository> mockReviewRepo;
        public Mock<IDateTimeProvider> mockDateTimeProvider;
        public IFixture fixture;

        public ReviewControllerFixture()
        {
            mockReviewRepo = new Mock<IReviewRepository>();
            mockDateTimeProvider = new Mock<IDateTimeProvider>();
            reviewController = new ReviewController(mockReviewRepo.Object, mockDateTimeProvider.Object);
            dateTimeProvider = new DateTimeProvider();

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
