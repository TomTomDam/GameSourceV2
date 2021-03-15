using AutoFixture;
using GameSource.API.Controllers.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System;
using System.Linq;

namespace GameSource.Tests
{
    public class ControllerFixture : IDisposable
    {
        public DeveloperController developerController;
        public Mock<IDeveloperRepository> mockDeveloperRepo;
        public IFixture fixture;

        public ControllerFixture()
        {
            mockDeveloperRepo = new Mock<IDeveloperRepository>();
            developerController = new DeveloperController(mockDeveloperRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        public void Dispose()
        {
            mockDeveloperRepo.Invocations.Clear();
        }
    }
}
