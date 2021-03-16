using AutoFixture;
using GameSource.API.Areas.Admin;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.Admin
{
    public class UserRoleControllerFixture
    {
        public UserRoleController userController;
        public Mock<IUserRoleRepository> mockUserRoleRepo;
        public IFixture fixture;

        public UserRoleControllerFixture()
        {
            mockUserRoleRepo = new Mock<IUserRoleRepository>();
            userController = new UserRoleController(mockUserRoleRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
