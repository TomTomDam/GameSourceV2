using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource;
using GameSource.Services.GameSource.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Tests.Services
{
    [TestFixture]
    public class DeveloperServiceTests
    {
        public DeveloperService developerService;

        public Mock<IDeveloperRepository> mockDeveloperRepo;
        public Mock<IDeveloperService> mockDeveloperService;

        [SetUp]
        public void Setup()
        {
            mockDeveloperRepo = new Mock<IDeveloperRepository>();
            mockDeveloperService = new Mock<IDeveloperService>();

            developerService = new DeveloperService(mockDeveloperRepo.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockDeveloperService.Invocations.Clear();
            mockDeveloperRepo.Invocations.Clear();
        }

        [Test]
        public void GetAll_ReturnsListOfDevelopers()
        {
            var developerList = new List<Developer>()
            {
                new Developer { ID = 1, Name = "LucasArts"}
            };

            mockDeveloperRepo.Setup(x => x.GetAll()).Returns(developerList);
            mockDeveloperService.Setup(x => x.GetAll()).Returns(developerList);

            var result = developerService.GetAll();

            mockDeveloperRepo.Verify(x => x.GetAll(), Times.Once());
            //mockDeveloperService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Developer>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_ReturnsEmptyList()
        {
            mockDeveloperRepo.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Developer>());
            mockDeveloperService.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Developer>());

            var result = developerService.GetAll();

            mockDeveloperRepo.Verify(x => x.GetAll(), Times.Once());
            //mockDeveloperService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Developer>>(result);
            Assert.IsEmpty(result);
        }
    }
}
