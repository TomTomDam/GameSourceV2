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
    public class PlatformTypeRepositoryTests
    {
        public PlatformTypeRepository platformTypeRepo;
        public Mock<IPlatformTypeRepository> mockPlatformTypeRepo;
        public Mock<GameSource_DBContext> mockDBContext;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockPlatformTypeRepo = new Mock<IPlatformTypeRepository>();
            mockDBContext = new Mock<GameSource_DBContext>();
            platformTypeRepo = new PlatformTypeRepository(mockDBContext.Object);
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TearDown]
        public void Teardown()
        {
            mockPlatformTypeRepo.Invocations.Clear();
            mockDBContext.Invocations.Clear();
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfPlatformTypes()
        {
            var platformTypesList = fixture.Create<IEnumerable<PlatformType>>();

            mockPlatformTypeRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(platformTypesList);

            var result = await platformTypeRepo.GetAllAsync();

            mockPlatformTypeRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<PlatformType>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task GetAllAsync_ReturnsEmptyList()
        {
            mockPlatformTypeRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<PlatformType>());

            var result = await platformTypeRepo.GetAllAsync();

            mockPlatformTypeRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<PlatformType>>(result);
            Assert.IsEmpty(result);
        }
    }
}
