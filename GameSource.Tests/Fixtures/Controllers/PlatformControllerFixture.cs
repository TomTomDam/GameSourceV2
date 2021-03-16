using AutoFixture;
using GameSource.API.Controllers.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests
{
    public class PlatformControllerFixture
    {
        public PlatformController platformController;
        public Mock<IPlatformRepository> mockPlatformRepo;
        public IFixture fixture;

        public PlatformControllerFixture()
        {
            mockPlatformRepo = new Mock<IPlatformRepository>();
            platformController = new PlatformController(mockPlatformRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
