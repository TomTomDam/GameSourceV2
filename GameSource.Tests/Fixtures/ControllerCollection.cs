using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GameSource.Tests.Fixtures
{
    [CollectionDefinition("Controller collection")]
    public class ControllerCollection : ICollectionFixture<ControllerFixture>
    {

    }

    public class ControllerFixture
    {
        public ControllerFixture()
        {

        }
    }
}
