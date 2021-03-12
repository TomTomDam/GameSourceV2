using AutoFixture;
using GameSource.Infrastructure;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Tests.Repositories
{
    [TestFixture]
    public class PlatformRepositoryTests
    {
        public PlatformRepository platformRepo;
        public Mock<IPlatformRepository> mockPlatformRepo;
        public Mock<GameSource_DBContext> mockDBContext;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockPlatformRepo = new Mock<IPlatformRepository>();
            mockDBContext = new Mock<GameSource_DBContext>();
            platformRepo = new PlatformRepository(mockDBContext.Object);
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TearDown]
        public void Teardown()
        {
            mockPlatformRepo.Invocations.Clear();
            mockDBContext.Invocations.Clear();
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfPlatforms()
        {
            var platformsList = fixture.Create<IEnumerable<Platform>>();

            mockPlatformRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(platformsList);

            var result = await platformRepo.GetAllAsync();

            mockPlatformRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Platform>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task GetAllAsync_ReturnsEmptyList()
        {
            mockPlatformRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Platform>());

            var result = await platformRepo.GetAllAsync();

            mockPlatformRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Platform>>(result);
            Assert.IsEmpty(result);
        }
    }
}
