using AutoFixture;
using GameSource.API.Areas.Admin;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.Admin
{
    public class RoleControllerFixture
    {
        public RoleController roleController;
        public Mock<IRoleRepository> mockUserRoleRepo;
        public IFixture fixture;

        public RoleControllerFixture()
        {
            mockUserRoleRepo = new Mock<IRoleRepository>();
            roleController = new RoleController(mockUserRoleRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
