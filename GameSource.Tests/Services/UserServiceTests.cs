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
    public class UserServiceTests
    {
        public UserService userService;

        public Mock<IUserRepository> mockUserRepo;
        public Mock<IUserService> mockUserService;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockUserRepo = new Mock<IUserRepository>();
            mockUserService = new Mock<IUserService>();
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            userService = new UserService(mockUserRepo.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockUserRepo.Invocations.Clear();
            mockUserService.Invocations.Clear();
        }

        [Test]
        public void GetAll_ReturnsListOfUsers()
        {
            var usersList = fixture.Create<IEnumerable<User>>();

            mockUserRepo.Setup(x => x.GetAll()).Returns(usersList);
            mockUserService.Setup(x => x.GetAll()).Returns(usersList);

            var result = userService.GetAll();

            mockUserRepo.Verify(x => x.GetAll(), Times.Once());
            //mockUserService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<User>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_ReturnsEmptyList()
        {
            mockUserRepo.Setup(x => x.GetAll()).Returns(Enumerable.Empty<User>());
            mockUserService.Setup(x => x.GetAll()).Returns(Enumerable.Empty<User>());

            var result = userService.GetAll();

            mockUserRepo.Verify(x => x.GetAll(), Times.Once());
            //mockUserService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<User>>(result);
            Assert.IsEmpty(result);
        }
    }
}
