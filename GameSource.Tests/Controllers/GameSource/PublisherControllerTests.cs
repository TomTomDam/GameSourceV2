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
    public class PublisherControllerTests : IClassFixture<PublisherControllerFixture>, IDisposable
    {
        PublisherControllerFixture fixture;

        public PublisherControllerTests(PublisherControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockPublisherRepo.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsListOfPublishers()
        {
            var publisherList = fixture.fixture.Create<IEnumerable<Publisher>>();

            fixture.mockPublisherRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(publisherList);

            var result = await fixture.publisherController.GetAll();

            fixture.mockPublisherRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(publisherList, result.Data);
            Assert.True(result.NumberOfRows > 0);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsEmptyList()
        {
            fixture.mockPublisherRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Publisher>());

            var result = await fixture.publisherController.GetAll();

            fixture.mockPublisherRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(Enumerable.Empty<Publisher>(), result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_SuccessResponse_ReturnsPublisher()
        {
            var publisher = fixture.fixture.Create<Publisher>();

            fixture.mockPublisherRepo.Setup(x => x.GetByIDAsync(publisher.ID)).ReturnsAsync(publisher);

            var result = await fixture.publisherController.GetByID(publisher.ID);

            fixture.mockPublisherRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<Publisher>(result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenIDIs0()
        {
            fixture.mockPublisherRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Publisher)null);

            var result = await fixture.publisherController.GetByID(0);

            fixture.mockPublisherRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenPublisherIsNotFound()
        {
            fixture.mockPublisherRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((Publisher)null);

            var result = await fixture.publisherController.GetByID(1);

            fixture.mockPublisherRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }
        #endregion

        #region Insert
        [Fact]
        public async Task Insert_SuccessResponse_CreatesPublisher()
        {
            var publisher = fixture.fixture.Create<Publisher>();

            fixture.mockPublisherRepo.Setup(x => x.InsertAsync(publisher)).ReturnsAsync(1);

            var result = await fixture.publisherController.Insert(publisher);

            fixture.mockPublisherRepo.Verify(x => x.InsertAsync(It.IsAny<Publisher>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Insert_ErrorResponse_WhenPublisherIsNull()
        {
            fixture.mockPublisherRepo.Setup(x => x.InsertAsync(null)).ReturnsAsync(0);

            var result = await fixture.publisherController.Insert(null);

            fixture.mockPublisherRepo.Verify(x => x.InsertAsync(It.IsAny<Publisher>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_SuccessResponse_UpdatesPublisher()
        {
            var id = 1;
            var publisher = new Publisher
            {
                Name = "Nintendo"
            };
            var updatedPublisher = new Publisher
            {
                ID = 1,
                Name = "Electronic Arts"
            };

            fixture.mockPublisherRepo.Setup(x => x.GetByIDAsync(id)).ReturnsAsync(updatedPublisher);
            fixture.mockPublisherRepo.Setup(x => x.UpdateAsync(updatedPublisher)).ReturnsAsync(1);

            var result = await fixture.publisherController.Update(id, publisher);

            fixture.mockPublisherRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPublisherRepo.Verify(x => x.UpdateAsync(updatedPublisher), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(updatedPublisher, result.Data);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenIDIs0()
        {
            var publisher = fixture.fixture.Create<Publisher>();

            fixture.mockPublisherRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Publisher)null);

            var result = await fixture.publisherController.Update(0, publisher);

            fixture.mockPublisherRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockPublisherRepo.Verify(x => x.UpdateAsync(It.IsAny<Publisher>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenPublisherIsNull()
        {
            var publisher = fixture.fixture.Create<Publisher>();

            fixture.mockPublisherRepo.Setup(x => x.GetByIDAsync(publisher.ID)).ReturnsAsync(publisher);
            fixture.mockPublisherRepo.Setup(x => x.UpdateAsync(null)).ReturnsAsync(0);

            var result = await fixture.publisherController.Update(publisher.ID, publisher);

            fixture.mockPublisherRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPublisherRepo.Verify(x => x.UpdateAsync(It.IsAny<Publisher>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_SuccessResponse_DeletesPublisher()
        {
            var publisher = fixture.fixture.Create<Publisher>();

            fixture.mockPublisherRepo.Setup(x => x.GetByIDAsync(publisher.ID)).ReturnsAsync(publisher);
            fixture.mockPublisherRepo.Setup(x => x.DeleteAsync(publisher)).ReturnsAsync(1);

            var result = await fixture.publisherController.Delete(publisher.ID);

            fixture.mockPublisherRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPublisherRepo.Verify(x => x.DeleteAsync(It.IsAny<Publisher>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenIDIs0()
        {
            fixture.mockPublisherRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Publisher)null);

            var result = await fixture.publisherController.Delete(0);

            fixture.mockPublisherRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockPublisherRepo.Verify(x => x.DeleteAsync(It.IsAny<Publisher>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenPublisherIsNull()
        {
            var publisher = fixture.fixture.Create<Publisher>();

            fixture.mockPublisherRepo.Setup(x => x.GetByIDAsync(publisher.ID)).ReturnsAsync(publisher);
            fixture.mockPublisherRepo.Setup(x => x.DeleteAsync(null)).ReturnsAsync(0);

            var result = await fixture.publisherController.Delete(publisher.ID);

            fixture.mockPublisherRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPublisherRepo.Verify(x => x.DeleteAsync(It.IsAny<Publisher>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion
    }
}
