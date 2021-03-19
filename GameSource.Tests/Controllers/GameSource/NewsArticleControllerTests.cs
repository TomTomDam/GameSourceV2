using AutoFixture;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Tests.Fixtures.Controllers.GameSource;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameSource.Tests.Controllers.GameSource
{
    public class NewsArticleControllerTests : IClassFixture<NewsArticleControllerFixture>, IDisposable
    {
        NewsArticleControllerFixture fixture;

        public NewsArticleControllerTests(NewsArticleControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockNewsArticleRepo.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsListOfNewsArticles()
        {
            var newsArticleList = fixture.fixture.Create<IEnumerable<NewsArticle>>();

            fixture.mockNewsArticleRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(newsArticleList);

            var result = await fixture.newsArticleController.GetAll();

            fixture.mockNewsArticleRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(newsArticleList, result.Data);
            Assert.True(result.NumberOfRows > 0);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsEmptyList()
        {
            fixture.mockNewsArticleRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<NewsArticle>());

            var result = await fixture.newsArticleController.GetAll();

            fixture.mockNewsArticleRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(Enumerable.Empty<NewsArticle>(), result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_SuccessResponse_ReturnsNewsArticle()
        {
            var newsArticle = fixture.fixture.Create<NewsArticle>();

            fixture.mockNewsArticleRepo.Setup(x => x.GetByIDAsync(newsArticle.ID)).ReturnsAsync(newsArticle);

            var result = await fixture.newsArticleController.GetByID(newsArticle.ID);

            fixture.mockNewsArticleRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<NewsArticle>(result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenIDIs0()
        {
            fixture.mockNewsArticleRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((NewsArticle)null);

            var result = await fixture.newsArticleController.GetByID(0);

            fixture.mockNewsArticleRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenNewsArticleIsNotFound()
        {
            fixture.mockNewsArticleRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((NewsArticle)null);

            var result = await fixture.newsArticleController.GetByID(1);

            fixture.mockNewsArticleRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }
        #endregion

        #region Insert
        [Fact]
        public async Task Insert_SuccessResponse_CreatesNewsArticle()
        {
            var newsArticle = fixture.fixture.Create<NewsArticle>();

            fixture.mockNewsArticleRepo.Setup(x => x.InsertAsync(newsArticle)).ReturnsAsync(true);

            var result = await fixture.newsArticleController.Insert(newsArticle);

            fixture.mockNewsArticleRepo.Verify(x => x.InsertAsync(It.IsAny<NewsArticle>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Insert_ErrorResponse_WhenNewsArticleIsNull()
        {
            fixture.mockNewsArticleRepo.Setup(x => x.InsertAsync(null)).ReturnsAsync(false);

            var result = await fixture.newsArticleController.Insert(null);

            fixture.mockNewsArticleRepo.Verify(x => x.InsertAsync(It.IsAny<NewsArticle>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_SuccessResponse_UpdatesNewsArticle()
        {
            var id = 1;
            var newsArticle = new NewsArticle
            {
                Title = "Welcome to GameSource!",
                Body = "Enjoy your stay!",
                CategoryID = 1,
                CoverImageFilePath = "welcometogamesource",
                //DateModified = DateTime.Now
            };
            var updatedNewsArticle = new NewsArticle
            {
                ID = 1,
                Title = "GameSource is cool!",
                Body = "Check it out!",
                CategoryID = 2,
                CoverImageFilePath = "gamesourceiscool"
                //DateModified = DateTime.Now
            };

            fixture.mockNewsArticleRepo.Setup(x => x.GetByIDAsync(id)).ReturnsAsync(updatedNewsArticle);
            fixture.mockNewsArticleRepo.Setup(x => x.UpdateAsync(updatedNewsArticle)).ReturnsAsync(true);

            var result = await fixture.newsArticleController.Update(id, newsArticle);

            fixture.mockNewsArticleRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockNewsArticleRepo.Verify(x => x.UpdateAsync(updatedNewsArticle), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(updatedNewsArticle, result.Data);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenIDIs0()
        {
            var newsArticle = fixture.fixture.Create<NewsArticle>();

            fixture.mockNewsArticleRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((NewsArticle)null);

            var result = await fixture.newsArticleController.Update(0, newsArticle);

            fixture.mockNewsArticleRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockNewsArticleRepo.Verify(x => x.UpdateAsync(It.IsAny<NewsArticle>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenNewsArticleIsNotFound()
        {
            var newsArticle = fixture.fixture.Create<NewsArticle>();

            fixture.mockNewsArticleRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((NewsArticle)null);

            var result = await fixture.newsArticleController.Update(newsArticle.ID, newsArticle);

            fixture.mockNewsArticleRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockNewsArticleRepo.Verify(x => x.UpdateAsync(It.IsAny<NewsArticle>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_SuccessResponse_DeletesNewsArticle()
        {
            var newsArticle = fixture.fixture.Create<NewsArticle>();

            fixture.mockNewsArticleRepo.Setup(x => x.GetByIDAsync(newsArticle.ID)).ReturnsAsync(newsArticle);
            fixture.mockNewsArticleRepo.Setup(x => x.DeleteAsync(newsArticle)).ReturnsAsync(true);

            var result = await fixture.newsArticleController.Delete(newsArticle.ID);

            fixture.mockNewsArticleRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockNewsArticleRepo.Verify(x => x.DeleteAsync(It.IsAny<NewsArticle>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenIDIs0()
        {
            fixture.mockNewsArticleRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((NewsArticle)null);

            var result = await fixture.newsArticleController.Delete(0);

            fixture.mockNewsArticleRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockNewsArticleRepo.Verify(x => x.DeleteAsync(It.IsAny<NewsArticle>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenNewsArticleIsNotFound()
        {
            var newsArticle = fixture.fixture.Create<NewsArticle>();

            fixture.mockNewsArticleRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((NewsArticle)null);

            var result = await fixture.newsArticleController.Delete(newsArticle.ID);

            fixture.mockNewsArticleRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockNewsArticleRepo.Verify(x => x.DeleteAsync(It.IsAny<NewsArticle>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenNewsArticleIsNotDeleted()
        {
            var newsArticle = fixture.fixture.Create<NewsArticle>();

            fixture.mockNewsArticleRepo.Setup(x => x.GetByIDAsync(newsArticle.ID)).ReturnsAsync(newsArticle);
            fixture.mockNewsArticleRepo.Setup(x => x.DeleteAsync(newsArticle)).ReturnsAsync(false);

            var result = await fixture.newsArticleController.Delete(newsArticle.ID);

            fixture.mockNewsArticleRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockNewsArticleRepo.Verify(x => x.DeleteAsync(It.IsAny<NewsArticle>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion
    }
}
