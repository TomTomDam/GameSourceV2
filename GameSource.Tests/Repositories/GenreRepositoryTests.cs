using AutoFixture;
using GameSource.Infrastructure;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Tests.Repositories
{
    [TestFixture]
    public class GenreRepositoryTests
    {
        public GenreRepository genreRepo;
        public Mock<IGenreRepository> mockGenreRepo;
        public Mock<GameSource_DBContext> mockDBContext;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockGenreRepo = new Mock<IGenreRepository>();
            mockDBContext = new Mock<GameSource_DBContext>();
            genreRepo = new GenreRepository(mockDBContext.Object);
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [TearDown]
        public void Teardown()
        {
            mockGenreRepo.Invocations.Clear();
            mockDBContext.Invocations.Clear();
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfGenres()
        {
            var genresList = fixture.Create<IEnumerable<Genre>>();

            mockGenreRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(genresList);

            var result = await genreRepo.GetAllAsync();

            mockGenreRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Genre>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task GetAllAsync_ReturnsEmptyList()
        {
            mockGenreRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Genre>());

            var result = await genreRepo.GetAllAsync();

            mockGenreRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Genre>>(result);
            Assert.IsEmpty(result);
        }
    }
}
