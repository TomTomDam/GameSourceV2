using GameSource.Tests.Fixtures.Controllers.GameSourceUser;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameSource.Tests.Controllers.GameSourceUser
{
    public class UserControllerTests : IClassFixture<UserControllerFixture>, IDisposable
    {
        UserControllerFixture _fixture;

        public UserControllerTests(UserControllerFixture fixture)
        {
            _fixture = fixture;
        }

        public void Dispose()
        {
            _fixture.mockUserRepo.Invocations.Clear();
        }
    }
}
