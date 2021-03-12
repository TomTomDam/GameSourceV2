using AutoFixture;
using AutoFixture.Kernel;
using GameSource.Infrastructure;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using GameSource.Models.GameSource;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSource.Tests.Repositories
{
    [TestFixture]
    public class PublisherRepositoryTests
    {
        public PublisherRepository publisherRepo;
        public Mock<IPublisherRepository> mockPublisherRepo;
        public Mock<GameSource_DBContext> mockDBContext;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockPublisherRepo = new Mock<IPublisherRepository>();
            mockDBContext = new Mock<GameSource_DBContext>();
            publisherRepo = new PublisherRepository(mockDBContext.Object);
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Register((Mock<IFormFile> f) => f.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockPublisherRepo.Invocations.Clear();
            mockDBContext.Invocations.Clear();
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfPublishers()
        {
            var publishersList = fixture.Create<IEnumerable<Publisher>>();

            mockPublisherRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(publishersList);

            var result = await publisherRepo.GetAllAsync();

            mockPublisherRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Publisher>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public async Task GetAllAsync_ReturnsEmptyList()
        {
            mockPublisherRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Publisher>());

            var result = await publisherRepo.GetAllAsync();

            mockPublisherRepo.Verify(x => x.GetAllAsync(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Publisher>>(result);
            Assert.IsEmpty(result);
        }
    }
}
