using AutoFixture;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameSource.Tests.Controllers
{
    public class ReviewCommentControllerTests : IClassFixture<ReviewCommentControllerFixture>, IDisposable
    {
        ReviewCommentControllerFixture fixture;

        public ReviewCommentControllerTests(ReviewCommentControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockReviewCommentRepo.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsListOfReviewComments()
        {
            var reviewCommentList = fixture.fixture.Create<IEnumerable<ReviewComment>>();

            fixture.mockReviewCommentRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(reviewCommentList);

            var result = await fixture.reviewCommentController.GetAll();

            fixture.mockReviewCommentRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(reviewCommentList, result.Data);
            Assert.True(result.NumberOfRows > 0);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsEmptyList()
        {
            fixture.mockReviewCommentRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<ReviewComment>());

            var result = await fixture.reviewCommentController.GetAll();

            fixture.mockReviewCommentRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(Enumerable.Empty<ReviewComment>(), result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_SuccessResponse_ReturnsReviewComment()
        {
            var reviewComment = fixture.fixture.Create<ReviewComment>();

            fixture.mockReviewCommentRepo.Setup(x => x.GetByIDAsync(reviewComment.ID)).ReturnsAsync(reviewComment);

            var result = await fixture.reviewCommentController.GetByID(reviewComment.ID);

            fixture.mockReviewCommentRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<ReviewComment>(result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenIDIs0()
        {
            fixture.mockReviewCommentRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((ReviewComment)null);

            var result = await fixture.reviewCommentController.GetByID(0);

            fixture.mockReviewCommentRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Insert
        [Fact]
        public async Task Insert_SuccessResponse_CreatesReviewComment()
        {
            var reviewComment = fixture.fixture.Create<ReviewComment>();

            fixture.mockReviewCommentRepo.Setup(x => x.InsertAsync(reviewComment)).ReturnsAsync(1);

            var result = await fixture.reviewCommentController.Insert(reviewComment);

            fixture.mockReviewCommentRepo.Verify(x => x.InsertAsync(It.IsAny<ReviewComment>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Insert_ErrorResponse_WhenReviewCommentIsNull()
        {
            fixture.mockReviewCommentRepo.Setup(x => x.InsertAsync(null)).ReturnsAsync(0);

            var result = await fixture.reviewCommentController.Insert(null);

            fixture.mockReviewCommentRepo.Verify(x => x.InsertAsync(It.IsAny<ReviewComment>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_SuccessResponse_UpdatesReviewComment()
        {
            var id = 1;
            var reviewComment = new ReviewComment
            {
                Body = "Great review!"
            };
            var updatedReviewComment = new ReviewComment
            {
                ID = 1,
                Body = "Nice review!"
            };

            fixture.mockReviewCommentRepo.Setup(x => x.GetByIDAsync(id)).ReturnsAsync(updatedReviewComment);
            fixture.mockReviewCommentRepo.Setup(x => x.UpdateAsync(updatedReviewComment)).ReturnsAsync(1);

            var result = await fixture.reviewCommentController.Update(id, reviewComment);

            fixture.mockReviewCommentRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockReviewCommentRepo.Verify(x => x.UpdateAsync(updatedReviewComment), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(updatedReviewComment, result.Data);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenIDIs0()
        {
            var reviewComment = fixture.fixture.Create<ReviewComment>();

            fixture.mockReviewCommentRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((ReviewComment)null);

            var result = await fixture.reviewCommentController.Update(0, reviewComment);

            fixture.mockReviewCommentRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockReviewCommentRepo.Verify(x => x.UpdateAsync(It.IsAny<ReviewComment>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenReviewCommentIsNull()
        {
            var reviewComment = fixture.fixture.Create<ReviewComment>();

            fixture.mockReviewCommentRepo.Setup(x => x.GetByIDAsync(reviewComment.ID)).ReturnsAsync(reviewComment);
            fixture.mockReviewCommentRepo.Setup(x => x.UpdateAsync(null)).ReturnsAsync(0);

            var result = await fixture.reviewCommentController.Update(reviewComment.ID, reviewComment);

            fixture.mockReviewCommentRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockReviewCommentRepo.Verify(x => x.UpdateAsync(It.IsAny<ReviewComment>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_SuccessResponse_DeletesReviewComment()
        {
            var reviewComment = fixture.fixture.Create<ReviewComment>();

            fixture.mockReviewCommentRepo.Setup(x => x.GetByIDAsync(reviewComment.ID)).ReturnsAsync(reviewComment);
            fixture.mockReviewCommentRepo.Setup(x => x.DeleteAsync(reviewComment)).ReturnsAsync(1);

            var result = await fixture.reviewCommentController.Delete(reviewComment.ID);

            fixture.mockReviewCommentRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockReviewCommentRepo.Verify(x => x.DeleteAsync(It.IsAny<ReviewComment>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenIDIs0()
        {
            fixture.mockReviewCommentRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((ReviewComment)null);

            var result = await fixture.reviewCommentController.Delete(0);

            fixture.mockReviewCommentRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockReviewCommentRepo.Verify(x => x.DeleteAsync(It.IsAny<ReviewComment>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenReviewCommentIsNull()
        {
            var reviewComment = fixture.fixture.Create<ReviewComment>();

            fixture.mockReviewCommentRepo.Setup(x => x.GetByIDAsync(reviewComment.ID)).ReturnsAsync(reviewComment);
            fixture.mockReviewCommentRepo.Setup(x => x.DeleteAsync(null)).ReturnsAsync(0);

            var result = await fixture.reviewCommentController.Delete(reviewComment.ID);

            fixture.mockReviewCommentRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockReviewCommentRepo.Verify(x => x.DeleteAsync(It.IsAny<ReviewComment>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion
    }
}
