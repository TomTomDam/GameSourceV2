﻿using AutoFixture;
using GameSource.API.Controllers.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Moq;
using System.Linq;

namespace GameSource.Tests
{
    public class NewsArticleCategoryControllerFixture
    {
        public NewsArticleCategoryController newsArticleCategoryController;
        public Mock<INewsArticleCategoryRepository> mockNewsArticleCategoryRepo;
        public IFixture fixture;

        public NewsArticleCategoryControllerFixture()
        {
            mockNewsArticleCategoryRepo = new Mock<INewsArticleCategoryRepository>();
            newsArticleCategoryController = new NewsArticleCategoryController(mockNewsArticleCategoryRepo.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}