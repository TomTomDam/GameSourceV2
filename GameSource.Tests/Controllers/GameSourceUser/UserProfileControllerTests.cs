using GameSource.Tests.Fixtures.Controllers.GameSourceUser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameSource.Tests.Controllers.GameSourceUser
{
    public class UserProfileControllerTests : IClassFixture<UserProfileControllerFixture>, IDisposable
    {
        UserProfileControllerFixture _fixture;

        public UserProfileControllerTests(UserProfileControllerFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture.mockUserProfileRepo.Invocations.Clear();
        }
    }
}
