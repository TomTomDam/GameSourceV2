using GameSource.Controllers.GameSource;
using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Tests.Controllers
{
    [TestFixture]
    public class DeveloperControllerTest
    {
        public DeveloperController developerController;
        public DeveloperService developerService;

        public Mock<IDeveloperRepository> mockDeveloperRepo;
        public Mock<IDeveloperService> mockDeveloperService;

        [SetUp]
        public void Setup()
        {
            mockDeveloperRepo = new Mock<IDeveloperRepository>();
            mockDeveloperService = new Mock<IDeveloperService>();

            developerService = new DeveloperService(mockDeveloperRepo.Object);
            developerController = new DeveloperController(mockDeveloperService.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockDeveloperService.Invocations.Clear();
            mockDeveloperRepo.Invocations.Clear();
        }

        [Test]
        public void Index_ReturnsListOfDevelopers()
        {
            var developerList = new List<Developer>()
            {
                new Developer { ID = 1, Name = "LucasArts"}
            };

            mockDeveloperRepo.Setup(x => x.GetAll()).Returns(developerList);
            mockDeveloperService.Setup(x => x.GetAll()).Returns(developerList);

            var result = developerController.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOf<IActionResult>(result);
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
