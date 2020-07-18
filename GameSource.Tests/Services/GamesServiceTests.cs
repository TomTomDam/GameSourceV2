using AutoFixture;
using AutoFixture.AutoMoq;
using GameSource.Controllers.GameSource;
using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource;
using GameSource.Services.GameSource.Contracts;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Tests.Services
{
    [TestFixture]
    public class GamesServiceTests
    {
        public GameService gameService;

        public Mock<IGameRepository> mockGameRepo;
        public Mock<IGameService> mockGameService;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockGameRepo = new Mock<IGameRepository>();
            mockGameService = new Mock<IGameService>();
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            gameService = new GameService(mockGameRepo.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockGameRepo.Invocations.Clear();
            mockGameService.Invocations.Clear();
        }

        [Test]
        public void GetAll_ReturnsListOfGames()
        {
            var gamesList = fixture.Create<IEnumerable<Game>>();

            mockGameRepo.Setup(x => x.GetAll()).Returns(gamesList);
            mockGameService.Setup(x => x.GetAll()).Returns(gamesList);

            var result = gameService.GetAll();

            mockGameRepo.Verify(x => x.GetAll(), Times.Once());
            //mockGameService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Game>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_ReturnsEmptyList()
        {
            mockGameRepo.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Game>());
            mockGameService.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Game>());

            var result = gameService.GetAll();

            mockGameRepo.Verify(x => x.GetAll(), Times.Once());
            //mockGameService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Game>>(result);
            Assert.IsEmpty(result);
        }
    }
}
