using AutoFixture;
using GameSource.Data.Repositories.GameSourceUser.Contracts;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Tests.Services
{
    [TestFixture]
    public class UserStatusServiceTests
    {
        public UserStatusService userStatusService;

        public Mock<IUserStatusRepository> mockUserStatusRepo;
        public Mock<IUserStatusService> mockUserStatusService;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockUserStatusRepo = new Mock<IUserStatusRepository>();
            mockUserStatusService = new Mock<IUserStatusService>();
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            userStatusService = new UserStatusService(mockUserStatusRepo.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockUserStatusRepo.Invocations.Clear();
            mockUserStatusService.Invocations.Clear();
        }

        [Test]
        public void GetAll_ReturnsListOfUserStatuss()
        {
            var userStatusList = fixture.Create<IEnumerable<UserStatus>>();

            mockUserStatusRepo.Setup(x => x.GetAll()).Returns(userStatusList);
            mockUserStatusService.Setup(x => x.GetAll()).Returns(userStatusList);

            var result = userStatusService.GetAll();

            mockUserStatusRepo.Verify(x => x.GetAll(), Times.Once());
            //mockUserStatusService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<UserStatus>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_ReturnsEmptyList()
        {
            mockUserStatusRepo.Setup(x => x.GetAll()).Returns(Enumerable.Empty<UserStatus>());
            mockUserStatusService.Setup(x => x.GetAll()).Returns(Enumerable.Empty<UserStatus>());

            var result = userStatusService.GetAll();

            mockUserStatusRepo.Verify(x => x.GetAll(), Times.Once());
            //mockUserStatusService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<UserStatus>>(result);
            Assert.IsEmpty(result);
        }
    }
}
