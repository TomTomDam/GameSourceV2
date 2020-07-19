using AutoFixture;
using GameSource.Data.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource;
using GameSource.Services.GameSource.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameSource.Tests.Services
{
    [TestFixture]
    public class GenreServiceTests
    {
        public GenreService genreService;

        public Mock<IGenreRepository> mockGenreRepo;
        public Mock<IGenreService> mockGenreService;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockGenreRepo = new Mock<IGenreRepository>();
            mockGenreService = new Mock<IGenreService>();
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            genreService = new GenreService(mockGenreRepo.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockGenreRepo.Invocations.Clear();
            mockGenreService.Invocations.Clear();
        }

        [Test]
        public void GetAll_ReturnsListOfGenres()
        {
            var genresList = fixture.Create<IEnumerable<Genre>>();

            mockGenreRepo.Setup(x => x.GetAll()).Returns(genresList);
            mockGenreService.Setup(x => x.GetAll()).Returns(genresList);

            var result = genreService.GetAll();

            mockGenreRepo.Verify(x => x.GetAll(), Times.Once());
            //mockGenreService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Genre>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_ReturnsEmptyList()
        {
            mockGenreRepo.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Genre>());
            mockGenreService.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Genre>());

            var result = genreService.GetAll();

            mockGenreRepo.Verify(x => x.GetAll(), Times.Once());
            //mockGenreService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Genre>>(result);
            Assert.IsEmpty(result);
        }
    }
}
