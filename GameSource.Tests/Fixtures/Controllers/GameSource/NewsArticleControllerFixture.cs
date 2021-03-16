using AutoFixture;
using GameSource.API.Controllers.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests.Fixtures.Controllers.GameSource
{
    public class NewsArticleControllerFixture
    {
        public NewsArticleController newsArticleController;
        public Mock<INewsArticleRepository> mockNewsArticleRepo;
        public IFixture fixture;

        public NewsArticleControllerFixture()
        {
            mockNewsArticleRepo = new Mock<INewsArticleRepository>();
            newsArticleController = new NewsArticleController(mockNewsArticleRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
