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
    public class DeveloperRepositoryTests
    {
        public DeveloperRepository developerRepository;
        public Mock<IDeveloperRepository> mockDeveloperRepo;
        public Mock<GameSource_DBContext> mockDBContext;

        [SetUp]
        public void Setup()
        {
            mockDeveloperRepo = new Mock<IDeveloperRepository>();
            mockDBContext = new Mock<GameSource_DBContext>();
            developerRepository = new DeveloperRepository(mockDBContext.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockDeveloperRepo.Invocations.Clear();
            mockDBContext.Invocations.Clear();
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfDevelopers()
        {
            var developerList = new List<Developer>()
            {
                new Developer { ID = 1, Name = "LucasArts"}
            };

            mockDeveloperRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(developerList);

            var result = await developerRepository.GetAllAsync();

            mockDeveloperRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Developer>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task GetAllAsync_ReturnsEmptyList()
        {
            mockDeveloperRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Developer>());

            var result = await developerRepository.GetAllAsync();

            mockDeveloperRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Developer>>(result);
            Assert.IsEmpty(result);
        }
    }
}
