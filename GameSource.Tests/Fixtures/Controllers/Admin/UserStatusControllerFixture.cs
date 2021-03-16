using AutoFixture;
using GameSource.API.Areas.Admin;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.Admin
{
    public class UserStatusControllerFixture
    {
        public UserStatusController userController;
        public Mock<IUserStatusRepository> mockUserStatusRepo;
        public IFixture fixture;

        public UserStatusControllerFixture()
        {
            mockUserStatusRepo = new Mock<IUserStatusRepository>();
            userController = new UserStatusController(mockUserStatusRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
