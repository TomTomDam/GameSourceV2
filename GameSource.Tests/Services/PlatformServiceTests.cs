using AutoFixture;
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
    public class PlatformServiceTests
    {
        public PlatformService platformService;

        public Mock<IPlatformRepository> mockPlatformRepo;
        public Mock<IPlatformService> mockPlatformService;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockPlatformRepo = new Mock<IPlatformRepository>();
            mockPlatformService = new Mock<IPlatformService>();
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            platformService = new PlatformService(mockPlatformRepo.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockPlatformRepo.Invocations.Clear();
            mockPlatformService.Invocations.Clear();
        }

        [Test]
        public void GetAll_ReturnsListOfPlatforms()
        {
            var platformsList = fixture.Create<IEnumerable<Platform>>();

            mockPlatformRepo.Setup(x => x.GetAll()).Returns(platformsList);
            mockPlatformService.Setup(x => x.GetAll()).Returns(platformsList);

            var result = platformService.GetAll();

            mockPlatformRepo.Verify(x => x.GetAll(), Times.Once());
            //mockPlatformService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Platform>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_ReturnsEmptyList()
        {
            mockPlatformRepo.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Platform>());
            mockPlatformService.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Platform>());

            var result = platformService.GetAll();

            mockPlatformRepo.Verify(x => x.GetAll(), Times.Once());
            //mockPlatformService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Platform>>(result);
            Assert.IsEmpty(result);
        }
    }
}
