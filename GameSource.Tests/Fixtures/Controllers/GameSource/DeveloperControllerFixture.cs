using AutoFixture;
using GameSource.API.Controllers.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.GameSource
{
    public class DeveloperControllerFixture
    {
        public DeveloperController developerController;
        public Mock<IDeveloperRepository> mockDeveloperRepo;
        public IFixture fixture;

        public DeveloperControllerFixture()
        {
            mockDeveloperRepo = new Mock<IDeveloperRepository>();
            developerController = new DeveloperController(mockDeveloperRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
