using AutoFixture;
using GameSource.API.Controllers;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.GameSource
{
    public class PlatformTypeControllerFixture
    {
        public PlatformTypeController platformTypeController;
        public Mock<IPlatformTypeRepository> mockPlatformTypeRepo;
        public IFixture fixture;

        public PlatformTypeControllerFixture()
        {
            mockPlatformTypeRepo = new Mock<IPlatformTypeRepository>();
            platformTypeController = new PlatformTypeController(mockPlatformTypeRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
