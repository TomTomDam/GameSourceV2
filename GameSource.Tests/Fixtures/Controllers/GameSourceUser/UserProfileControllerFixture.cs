using AutoFixture;
using GameSource.API.Areas.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.GameSourceUser
{
    public class UserProfileControllerFixture
    {
        public UserProfileController userProfileController;
        public Mock<IUserProfileRepository> mockUserProfileRepo;
        public IFixture fixture;

        public UserProfileControllerFixture()
        {
            mockUserProfileRepo = new Mock<IUserProfileRepository>();
            userProfileController = new UserProfileController(mockUserProfileRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
