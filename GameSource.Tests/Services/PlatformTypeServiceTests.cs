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
    public class PlatformTypeServiceTests
    {
        public PlatformTypeService platformTypeService;

        public Mock<IPlatformTypeRepository> mockPlatformTypeRepo;
        public Mock<IPlatformTypeService> mockPlatformTypeService;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockPlatformTypeRepo = new Mock<IPlatformTypeRepository>();
            mockPlatformTypeService = new Mock<IPlatformTypeService>();
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            platformTypeService = new PlatformTypeService(mockPlatformTypeRepo.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockPlatformTypeRepo.Invocations.Clear();
            mockPlatformTypeService.Invocations.Clear();
        }

        [Test]
        public void GetAll_ReturnsListOfPlatformTypes()
        {
            var platformTypesList = fixture.Create<IEnumerable<PlatformType>>();

            mockPlatformTypeRepo.Setup(x => x.GetAll()).Returns(platformTypesList);
            mockPlatformTypeService.Setup(x => x.GetAll()).Returns(platformTypesList);

            var result = platformTypeService.GetAll();

            mockPlatformTypeRepo.Verify(x => x.GetAll(), Times.Once());
            //mockPlatformTypeService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<PlatformType>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_ReturnsEmptyList()
        {
            mockPlatformTypeRepo.Setup(x => x.GetAll()).Returns(Enumerable.Empty<PlatformType>());
            mockPlatformTypeService.Setup(x => x.GetAll()).Returns(Enumerable.Empty<PlatformType>());

            var result = platformTypeService.GetAll();

            mockPlatformTypeRepo.Verify(x => x.GetAll(), Times.Once());
            //mockPlatformTypeService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<PlatformType>>(result);
            Assert.IsEmpty(result);
        }
    }
}
