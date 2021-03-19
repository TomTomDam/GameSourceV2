using AutoFixture;
using GameSource.API.Areas.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Moq;
using System.Linq;


namespace GameSource.Tests.Fixtures.Controllers.GameSourceUser
{
    public class UserProfileCommentControllerFixture
    {
        public UserProfileCommentController userProfileCommentController;
        public Mock<IUserProfileCommentRepository> mockUserProfileCommentRepo;
        public IFixture fixture;

        public UserProfileCommentControllerFixture()
        {
            mockUserProfileCommentRepo = new Mock<IUserProfileCommentRepository>();
            userProfileCommentController = new UserProfileCommentController(mockUserProfileCommentRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
