using GameSource.Tests.Fixtures.Controllers.GameSourceUser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameSource.Tests.Controllers.GameSourceUser
{
    public class UserProfileCommentControllerTests : IClassFixture<UserProfileCommentControllerFixture>, IDisposable
    {
        UserProfileCommentControllerFixture _fixture;

        public UserProfileCommentControllerTests(UserProfileCommentControllerFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture.mockUserProfileCommentRepo.Invocations.Clear();
        }
    }
}
