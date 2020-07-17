using GameSource.Data;
using GameSource.Data.Repositories.GameSource;
using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.EntityFrameworkCore;
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
        private DeveloperRepository developerRepo;
        private GameSource_DBContext context;
        private DbContextOptions<GameSource_DBContext> options;

        private Mock<IDeveloperService> mockDeveloperService;
        private Mock<IDeveloperRepository> mockDeveloperRepo;

        [SetUp]
        public void Setup()
        {
            context = new GameSource_DBContext(options);
            developerRepo = new DeveloperRepository(context);
            developerService = new DeveloperService(developerRepo);
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
