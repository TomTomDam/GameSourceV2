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
    public class PublisherServiceTests
    {
        public PublisherService publisherService;

        public Mock<IPublisherRepository> mockPublisherRepo;
        public Mock<IPublisherService> mockPublisherService;
        public IFixture fixture;

        [SetUp]
        public void Setup()
        {
            mockPublisherRepo = new Mock<IPublisherRepository>();
            mockPublisherService = new Mock<IPublisherService>();
            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            publisherService = new PublisherService(mockPublisherRepo.Object);
        }

        [TearDown]
        public void Teardown()
        {
            mockPublisherRepo.Invocations.Clear();
            mockPublisherService.Invocations.Clear();
        }

        [Test]
        public void GetAll_ReturnsListOfPublishers()
        {
            var publishersList = fixture.Create<IEnumerable<Publisher>>();

            mockPublisherRepo.Setup(x => x.GetAll()).Returns(publishersList);
            mockPublisherService.Setup(x => x.GetAll()).Returns(publishersList);

            var result = publisherService.GetAll();

            mockPublisherRepo.Verify(x => x.GetAll(), Times.Once());
            //mockPublisherService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Publisher>>(result);
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void GetAll_ReturnsEmptyList()
        {
            mockPublisherRepo.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Publisher>());
            mockPublisherService.Setup(x => x.GetAll()).Returns(Enumerable.Empty<Publisher>());

            var result = publisherService.GetAll();

            mockPublisherRepo.Verify(x => x.GetAll(), Times.Once());
            //mockPublisherService.Verify(x => x.GetAll(), Times.Once());

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Publisher>>(result);
            Assert.IsEmpty(result);
        }
    }
}
