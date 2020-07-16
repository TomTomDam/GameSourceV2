using GameSource.Data;
using GameSource.Data.Repositories.GameSource;
using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource;
using GameSource.Services.GameSource.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Tests.Services
{
    [TestFixture]
    public class DeveloperServiceTests
    {
        private DeveloperService developerService;

        private Mock<IDeveloperService> mockDeveloperService;
        private Mock<IDeveloperRepository> mockDeveloperRepo;

        [SetUp]
        public void Setup()
        {
            mockDeveloperService = new Mock<IDeveloperService>();
            mockDeveloperRepo = new Mock<IDeveloperRepository>();
        }

        [Test]
        public void GetAll_ReturnsListOfDevelopers()
        {
            var result = developerService.GetAll();
            mockDeveloperService.Setup(x => x.GetAll()).Returns(result);

            Assert.IsNotEmpty(result);
        }
    }
}
