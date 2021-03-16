using AutoFixture;
using GameSource.API.Controllers.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests
{
    public class PublisherControllerFixture
    {
        public PublisherController publisherController;
        public Mock<IPublisherRepository> mockPublisherRepo;
        public IFixture fixture;

        public PublisherControllerFixture()
        {
            mockPublisherRepo = new Mock<IPublisherRepository>();
            publisherController = new PublisherController(mockPublisherRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
