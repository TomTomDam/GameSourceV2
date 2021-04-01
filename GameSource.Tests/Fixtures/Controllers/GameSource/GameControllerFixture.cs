using AutoFixture;
using GameSource.API.Controllers;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.GameSource
{
    public class GameControllerFixture
    {
        public GamesController gameController;
        public Mock<IGameRepository> mockGameRepo;
        public Mock<IPlatformRepository> mockPlatformRepo;
        public IFixture fixture;

        public GameControllerFixture()
        {
            mockGameRepo = new Mock<IGameRepository>();
            mockPlatformRepo = new Mock<IPlatformRepository>();
            gameController = new GamesController(mockGameRepo.Object, mockPlatformRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
