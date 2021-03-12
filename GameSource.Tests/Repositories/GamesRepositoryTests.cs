using AutoFixture;
using GameSource.Infrastructure;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Tests.Repositories
{
    [TestFixture]
    public class GamesRepositoryTests
    {
        public GameRepository gameRepo;
        public Mock<IGameRepository> mockGameRepo;
        public Mock<GameSource_DBContext> mockDBContext;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockGameRepo = new Mock<IGameRepository>();
            mockDBContext = new Mock<GameSource_DBContext>();
            gameRepo = new GameRepository(mockDBContext.Object);
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TearDown]
        public void Teardown()
        {
            mockGameRepo.Invocations.Clear();
            mockDBContext.Invocations.Clear();
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfGames()
        {
            var gamesList = fixture.Create<IEnumerable<Game>>();

            mockGameRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(gamesList);

            var result = await gameRepo.GetAllAsync();

            mockGameRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Game>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task GetAllAsync_ReturnsEmptyList()
        {
            mockGameRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Game>());

            var result = await gameRepo.GetAllAsync();

            mockGameRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Game>>(result);
            Assert.IsEmpty(result);
        }
    }
}
