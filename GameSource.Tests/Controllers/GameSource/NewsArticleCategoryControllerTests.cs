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
    public class NewsArticleCategoryControllerTests : IClassFixture<NewsArticleCategoryControllerFixture>, IDisposable
    {
        NewsArticleCategoryControllerFixture fixture;

        public NewsArticleCategoryControllerTests(NewsArticleCategoryControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockNewsArticleCategoryRepo.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_SucessResponse_ReturnsListOfNewsArticleCategory()
        {
            var newsArticleCategoryList = fixture.fixture.Create<IEnumerable<NewsArticleCategory>>();

            fixture.mockNewsArticleCategoryRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(newsArticleCategoryList);

            var result = await fixture.newsArticleCategoryController.GetAll();

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(newsArticleCategoryList, result.Data);
            Assert.True(result.NumberOfRows > 0);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsEmptyList()
        {
            fixture.mockNewsArticleCategoryRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<NewsArticleCategory>());

            var result = await fixture.newsArticleCategoryController.GetAll();

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(Enumerable.Empty<NewsArticleCategory>(), result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_SuccessResponse_ReturnsNewsArticleCategory()
        {
            var newsArticleCategory = fixture.fixture.Create<NewsArticleCategory>();

            fixture.mockNewsArticleCategoryRepo.Setup(x => x.GetByIDAsync(newsArticleCategory.ID)).ReturnsAsync(newsArticleCategory);

            var result = await fixture.newsArticleCategoryController.GetByID(newsArticleCategory.ID);

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<NewsArticleCategory>(result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenIDIs0()
        {
            fixture.mockNewsArticleCategoryRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((NewsArticleCategory)null);

            var result = await fixture.newsArticleCategoryController.GetByID(0);

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenNewsArticleIsNotFound()
        {
            fixture.mockNewsArticleCategoryRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((NewsArticleCategory)null);

            var result = await fixture.newsArticleCategoryController.GetByID(1);

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }
        #endregion

        #region Insert
        [Fact]
        public async Task Insert_SuccessResponse_CreatesNewsArticleCategory()
        {
            var newsArticleCategory = fixture.fixture.Create<NewsArticleCategory>();

            fixture.mockNewsArticleCategoryRepo.Setup(x => x.InsertAsync(newsArticleCategory)).ReturnsAsync(1);

            var result = await fixture.newsArticleCategoryController.Insert(newsArticleCategory);

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.InsertAsync(It.IsAny<NewsArticleCategory>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Insert_ErrorResponse_WhenNewsArticleCategoryIsNull()
        {
            fixture.mockNewsArticleCategoryRepo.Setup(x => x.InsertAsync(null)).ReturnsAsync(0);

            var result = await fixture.newsArticleCategoryController.Insert(null);

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.InsertAsync(It.IsAny<NewsArticleCategory>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_SuccessResponse_UpdatesNewsArticleCategory()
        {
            var id = 1;
            var newsArticleCategory = new NewsArticleCategory
            {
                Name = "Announcement"
            };
            var updatedNewsArticleCategory = new NewsArticleCategory
            {
                ID = 1,
                Name = "Game Update"
            };

            fixture.mockNewsArticleCategoryRepo.Setup(x => x.GetByIDAsync(id)).ReturnsAsync(updatedNewsArticleCategory);
            fixture.mockNewsArticleCategoryRepo.Setup(x => x.UpdateAsync(updatedNewsArticleCategory)).ReturnsAsync(1);

            var result = await fixture.newsArticleCategoryController.Update(id, newsArticleCategory);

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.GetByIDAsync(It.IsAny<int?>()), Times.Once);
            fixture.mockNewsArticleCategoryRepo.Verify(x => x.UpdateAsync(updatedNewsArticleCategory), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(updatedNewsArticleCategory, result.Data);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenIDIs0()
        {
            var newsArticleCategory = fixture.fixture.Create<NewsArticleCategory>();

            fixture.mockNewsArticleCategoryRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((NewsArticleCategory)null);

            var result = await fixture.newsArticleCategoryController.Update(0, newsArticleCategory);

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockNewsArticleCategoryRepo.Verify(x => x.UpdateAsync(It.IsAny<NewsArticleCategory>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenNewsArticleCategoryIsNull()
        {
            var newsArticleCategory = fixture.fixture.Create<NewsArticleCategory>();

            fixture.mockNewsArticleCategoryRepo.Setup(x => x.GetByIDAsync(newsArticleCategory.ID)).ReturnsAsync(newsArticleCategory);
            fixture.mockNewsArticleCategoryRepo.Setup(x => x.UpdateAsync(null)).ReturnsAsync(0);

            var result = await fixture.newsArticleCategoryController.Update(newsArticleCategory.ID, newsArticleCategory);

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockNewsArticleCategoryRepo.Verify(x => x.UpdateAsync(It.IsAny<NewsArticleCategory>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_SuccessResponse_DeletesNewsArticleCategory()
        {
            var newsArticleCategory = fixture.fixture.Create<NewsArticleCategory>();

            fixture.mockNewsArticleCategoryRepo.Setup(x => x.GetByIDAsync(newsArticleCategory.ID)).ReturnsAsync(newsArticleCategory);
            fixture.mockNewsArticleCategoryRepo.Setup(x => x.DeleteAsync(newsArticleCategory.ID)).ReturnsAsync(1);

            var result = await fixture.newsArticleCategoryController.Delete(newsArticleCategory.ID);

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockNewsArticleCategoryRepo.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenIDIs0()
        {
            fixture.mockNewsArticleCategoryRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((NewsArticleCategory)null);

            var result = await fixture.newsArticleCategoryController.Delete(0);

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockNewsArticleCategoryRepo.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenNewsArticleCategoryIsNull()
        {
            var newsArticleCategory = fixture.fixture.Create<NewsArticleCategory>();

            fixture.mockNewsArticleCategoryRepo.Setup(x => x.GetByIDAsync(newsArticleCategory.ID)).ReturnsAsync(newsArticleCategory);
            fixture.mockNewsArticleCategoryRepo.Setup(x => x.DeleteAsync(null)).ReturnsAsync(0);

            var result = await fixture.newsArticleCategoryController.Delete(newsArticleCategory.ID);

            fixture.mockNewsArticleCategoryRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockNewsArticleCategoryRepo.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion
    }
}
