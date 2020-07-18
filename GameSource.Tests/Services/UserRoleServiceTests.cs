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
    public class UserRoleServiceTests
    {
        public UserRoleService userRoleService;

        public Mock<IUserRoleRepository> mockUserRoleRepo;
        public Mock<IUserRoleService> mockUserRoleService;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockUserRoleRepo = new Mock<IUserRoleRepository>();
            mockUserRoleService = new Mock<IUserRoleService>();
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            userRoleService = new UserRoleService(mockUserRoleRepo.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockUserRoleRepo.Invocations.Clear();
            mockUserRoleService.Invocations.Clear();
        }

        [Test]
        public void GetAll_ReturnsListOfUserRoles()
        {
            var userRolesList = fixture.Create<IEnumerable<UserRole>>();

            mockUserRoleRepo.Setup(x => x.GetAll()).Returns(userRolesList);
            mockUserRoleService.Setup(x => x.GetAll()).Returns(userRolesList);

            var result = userRoleService.GetAll();

            mockUserRoleRepo.Verify(x => x.GetAll(), Times.Once());
            //mockUserRoleService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<UserRole>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_ReturnsEmptyList()
        {
            mockUserRoleRepo.Setup(x => x.GetAll()).Returns(Enumerable.Empty<UserRole>());
            mockUserRoleService.Setup(x => x.GetAll()).Returns(Enumerable.Empty<UserRole>());

            var result = userRoleService.GetAll();

            mockUserRoleRepo.Verify(x => x.GetAll(), Times.Once());
            //mockUserRoleService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<UserRole>>(result);
            Assert.IsEmpty(result);
        }
    }
}
