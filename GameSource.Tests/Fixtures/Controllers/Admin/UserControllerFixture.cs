using AutoFixture;
using GameSource.API.Areas.Admin;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.Admin
{
    public class UserControllerFixture
    {
        public UserController userController;
        public Mock<IUserRepository> mockUserRepo;
        public IFixture fixture;

        public UserControllerFixture()
        {
            mockUserRepo = new Mock<IUserRepository>();
            userController = new UserController(mockUserRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
