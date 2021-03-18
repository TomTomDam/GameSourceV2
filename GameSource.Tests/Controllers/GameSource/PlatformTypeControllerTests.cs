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
    public class PlatformTypeControllerTests : IClassFixture<PlatformTypeControllerFixture>, IDisposable
    {
        PlatformTypeControllerFixture fixture;

        public PlatformTypeControllerTests(PlatformTypeControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockPlatformTypeRepo.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsListOfPlatformTypes()
        {
            var platformTypeList = fixture.fixture.Create<IEnumerable<PlatformType>>();

            fixture.mockPlatformTypeRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(platformTypeList);

            var result = await fixture.platformTypeController.GetAll();

            fixture.mockPlatformTypeRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(platformTypeList, result.Data);
            Assert.True(result.NumberOfRows > 0);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsEmptyList()
        {
            fixture.mockPlatformTypeRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<PlatformType>());

            var result = await fixture.platformTypeController.GetAll();

            fixture.mockPlatformTypeRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(Enumerable.Empty<PlatformType>(), result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_SuccessResponse_ReturnsPlatformType()
        {
            var platformType = fixture.fixture.Create<PlatformType>();

            fixture.mockPlatformTypeRepo.Setup(x => x.GetByIDAsync(platformType.ID)).ReturnsAsync(platformType);

            var result = await fixture.platformTypeController.GetByID(platformType.ID);

            fixture.mockPlatformTypeRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<PlatformType>(result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenIDIs0()
        {
            fixture.mockPlatformTypeRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((PlatformType)null);

            var result = await fixture.platformTypeController.GetByID(0);

            fixture.mockPlatformTypeRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenPlatformTypeIsNotFound()
        {
            fixture.mockPlatformTypeRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((PlatformType)null);

            var result = await fixture.platformTypeController.GetByID(1);

            fixture.mockPlatformTypeRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }
        #endregion

        #region Insert
        [Fact]
        public async Task Insert_SuccessResponse_CreatesPlatformType()
        {
            var platformType = fixture.fixture.Create<PlatformType>();

            fixture.mockPlatformTypeRepo.Setup(x => x.InsertAsync(platformType)).ReturnsAsync(1);

            var result = await fixture.platformTypeController.Insert(platformType);

            fixture.mockPlatformTypeRepo.Verify(x => x.InsertAsync(It.IsAny<PlatformType>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Insert_ErrorResponse_WhenPlatformTypeIsNull()
        {
            fixture.mockPlatformTypeRepo.Setup(x => x.InsertAsync(null)).ReturnsAsync(0);

            var result = await fixture.platformTypeController.Insert(null);

            fixture.mockPlatformTypeRepo.Verify(x => x.InsertAsync(It.IsAny<PlatformType>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_SuccessResponse_UpdatesPlatformType()
        {
            var id = 1;
            var platformType = new PlatformType
            {
                Name = "Console"
            };
            var updatedPlatformType = new PlatformType
            {
                ID = 1,
                Name = "PC"
            };

            fixture.mockPlatformTypeRepo.Setup(x => x.GetByIDAsync(id)).ReturnsAsync(updatedPlatformType);
            fixture.mockPlatformTypeRepo.Setup(x => x.UpdateAsync(updatedPlatformType)).ReturnsAsync(1);

            var result = await fixture.platformTypeController.Update(id, platformType);

            fixture.mockPlatformTypeRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPlatformTypeRepo.Verify(x => x.UpdateAsync(updatedPlatformType), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(updatedPlatformType, result.Data);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenIDIs0()
        {
            var platformType = fixture.fixture.Create<PlatformType>();

            fixture.mockPlatformTypeRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((PlatformType)null);

            var result = await fixture.platformTypeController.Update(0, platformType);

            fixture.mockPlatformTypeRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockPlatformTypeRepo.Verify(x => x.UpdateAsync(It.IsAny<PlatformType>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenPlatformTypeIsNotFound()
        {
            var platformType = fixture.fixture.Create<PlatformType>();

            fixture.mockPlatformTypeRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((PlatformType)null);

            var result = await fixture.platformTypeController.Update(platformType.ID, platformType);

            fixture.mockPlatformTypeRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPlatformTypeRepo.Verify(x => x.UpdateAsync(It.IsAny<PlatformType>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_SuccessResponse_DeletesPlatformType()
        {
            var platformType = fixture.fixture.Create<PlatformType>();

            fixture.mockPlatformTypeRepo.Setup(x => x.GetByIDAsync(platformType.ID)).ReturnsAsync(platformType);
            fixture.mockPlatformTypeRepo.Setup(x => x.DeleteAsync(platformType)).ReturnsAsync(1);

            var result = await fixture.platformTypeController.Delete(platformType.ID);

            fixture.mockPlatformTypeRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPlatformTypeRepo.Verify(x => x.DeleteAsync(It.IsAny<PlatformType>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenIDIs0()
        {
            fixture.mockPlatformTypeRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((PlatformType)null);

            var result = await fixture.platformTypeController.Delete(0);

            fixture.mockPlatformTypeRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockPlatformTypeRepo.Verify(x => x.DeleteAsync(It.IsAny<PlatformType>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenPlatformTypeIsNotFound()
        {
            var platformType = fixture.fixture.Create<PlatformType>();

            fixture.mockPlatformTypeRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((PlatformType)null);

            var result = await fixture.platformTypeController.Delete(platformType.ID);

            fixture.mockPlatformTypeRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPlatformTypeRepo.Verify(x => x.DeleteAsync(It.IsAny<PlatformType>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenPlatformTypeIsNotDeleted()
        {
            var platformType = fixture.fixture.Create<PlatformType>();

            fixture.mockPlatformTypeRepo.Setup(x => x.GetByIDAsync(platformType.ID)).ReturnsAsync(platformType);
            fixture.mockPlatformTypeRepo.Setup(x => x.DeleteAsync(platformType)).ReturnsAsync(0);

            var result = await fixture.platformTypeController.Delete(platformType.ID);

            fixture.mockPlatformTypeRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPlatformTypeRepo.Verify(x => x.DeleteAsync(It.IsAny<PlatformType>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion
    }
}
