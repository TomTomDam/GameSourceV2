using GameSource.API.Controllers.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;

namespace GameSource.Tests.Controllers
{
    [TestFixture]
    public class DeveloperControllerTest
    {
        public DeveloperController developerController;
        public DeveloperRepository developerRepository;
        public Mock<IDeveloperRepository> mockDeveloperRepo;

        [SetUp]
        public void Setup()
        {
            mockDeveloperRepo = new Mock<IDeveloperRepository>();
            developerController = new DeveloperController(mockDeveloperRepo.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockDeveloperRepo.Invocations.Clear();
        }

        [Test]
        public async Task GetAll_ReturnsListOfDevelopers()
        {
            var developerList = new List<Developer>()
            {
                new Developer { ID = 1, Name = "LucasArts"}
            };

            mockDeveloperRepo.Setup(x => x.GetAll()).Returns(developerList);

            var result = await developerController.GetAll();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<ApiResponse>>(result);
        }
    }
}
