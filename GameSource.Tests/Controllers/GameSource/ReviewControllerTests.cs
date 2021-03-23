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
    public class ReviewControllerTests : IClassFixture<ReviewControllerFixture>, IDisposable
    {
        ReviewControllerFixture fixture;

        public ReviewControllerTests(ReviewControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockReviewRepo.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsListOfReviews()
        {
            var reviewList = fixture.fixture.Create<IEnumerable<Review>>();

            fixture.mockReviewRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(reviewList);

            var result = await fixture.reviewController.GetAll();

            fixture.mockReviewRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(reviewList, result.Data);
            Assert.True(result.NumberOfRows > 0);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsEmptyList()
        {
            fixture.mockReviewRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Review>());

            var result = await fixture.reviewController.GetAll();

            fixture.mockReviewRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(Enumerable.Empty<Review>(), result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_SuccessResponse_ReturnsReview()
        {
            var review = fixture.fixture.Create<Review>();

            fixture.mockReviewRepo.Setup(x => x.GetByIDAsync(review.ID)).ReturnsAsync(review);

            var result = await fixture.reviewController.GetByID(review.ID);

            fixture.mockReviewRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<Review>(result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenIDIs0()
        {
            fixture.mockReviewRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Review)null);

            var result = await fixture.reviewController.GetByID(0);

            fixture.mockReviewRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenReviewIsNotFound()
        {
            fixture.mockReviewRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((Review)null);

            var result = await fixture.reviewController.GetByID(1);

            fixture.mockReviewRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }
        #endregion

        #region Insert
        [Fact]
        public async Task Insert_SuccessResponse_CreatesReview()
        {
            var review = fixture.fixture.Create<Review>();

            fixture.mockReviewRepo.Setup(x => x.InsertAsync(review)).ReturnsAsync(true);

            var result = await fixture.reviewController.Insert(review);

            fixture.mockReviewRepo.Verify(x => x.InsertAsync(It.IsAny<Review>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Insert_ErrorResponse_WhenReviewIsNull()
        {
            fixture.mockReviewRepo.Setup(x => x.InsertAsync(null)).ReturnsAsync(false);

            var result = await fixture.reviewController.Insert(null);

            fixture.mockReviewRepo.Verify(x => x.InsertAsync(It.IsAny<Review>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_SuccessResponse_UpdatesReview()
        {
            var id = 1;
            var dateModified = new DateTime(2021, 03, 01);
            var review = new Review
            {
                Title = "Great game!",
                Body = "This is a great game!",
                HelpfulRating = 1,
                DateCreated = fixture.dateTimeProvider.Now
            };
            var updatedReview = new Review
            {
                ID = 1,
                Title = "Cool game!",
                Body = "This is a cool game!",
                HelpfulRating = 2,
                DateCreated = review.DateCreated
            };

            fixture.mockReviewRepo.Setup(x => x.GetByIDAsync(id)).ReturnsAsync(updatedReview);
            fixture.mockReviewRepo.Setup(x => x.UpdateAsync(updatedReview)).ReturnsAsync(true);
            fixture.mockDateTimeProvider.Setup(x => x.Now).Returns(dateModified);

            var result = await fixture.reviewController.Update(id, review);

            fixture.mockReviewRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockReviewRepo.Verify(x => x.UpdateAsync(updatedReview), Times.Once);
            fixture.mockDateTimeProvider.Verify(x => x.Now, Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(updatedReview, result.Data);
            Assert.Equal(dateModified, updatedReview.DateModified);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenIDIs0()
        {
            var review = fixture.fixture.Create<Review>();

            fixture.mockReviewRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Review)null);

            var result = await fixture.reviewController.Update(0, review);

            fixture.mockReviewRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockReviewRepo.Verify(x => x.UpdateAsync(It.IsAny<Review>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenReviewIsNotFound()
        {
            var review = fixture.fixture.Create<Review>();

            fixture.mockReviewRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((Review)null);

            var result = await fixture.reviewController.Update(review.ID, review);

            fixture.mockReviewRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockReviewRepo.Verify(x => x.UpdateAsync(It.IsAny<Review>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_SuccessResponse_DeletesReview()
        {
            var review = fixture.fixture.Create<Review>();

            fixture.mockReviewRepo.Setup(x => x.GetByIDAsync(review.ID)).ReturnsAsync(review);
            fixture.mockReviewRepo.Setup(x => x.DeleteAsync(review)).ReturnsAsync(true);

            var result = await fixture.reviewController.Delete(review.ID);

            fixture.mockReviewRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockReviewRepo.Verify(x => x.DeleteAsync(It.IsAny<Review>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenIDIs0()
        {
            fixture.mockReviewRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Review)null);

            var result = await fixture.reviewController.Delete(0);

            fixture.mockReviewRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockReviewRepo.Verify(x => x.DeleteAsync(It.IsAny<Review>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenReviewIsNotFound()
        {
            var review = fixture.fixture.Create<Review>();

            fixture.mockReviewRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((Review)null);

            var result = await fixture.reviewController.Delete(review.ID);

            fixture.mockReviewRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockReviewRepo.Verify(x => x.DeleteAsync(It.IsAny<Review>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenReviewIsNotDeleted()
        {
            var review = fixture.fixture.Create<Review>();

            fixture.mockReviewRepo.Setup(x => x.GetByIDAsync(review.ID)).ReturnsAsync(review);
            fixture.mockReviewRepo.Setup(x => x.DeleteAsync(review)).ReturnsAsync(false);

            var result = await fixture.reviewController.Delete(review.ID);

            fixture.mockReviewRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockReviewRepo.Verify(x => x.DeleteAsync(It.IsAny<Review>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion
    }
}
