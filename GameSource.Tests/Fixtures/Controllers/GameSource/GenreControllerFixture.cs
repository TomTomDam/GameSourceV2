using AutoFixture;
using GameSource.API.Controllers;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.GameSource
{
    public class GenreControllerFixture
    {
        public GenreController genreController;
        public Mock<IGenreRepository> mockGenreRepo;
        public IFixture fixture;

        public GenreControllerFixture()
        {
            mockGenreRepo = new Mock<IGenreRepository>();
            genreController = new GenreController(mockGenreRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
