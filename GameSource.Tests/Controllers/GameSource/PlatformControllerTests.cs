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
    public class PlatformControllerTests : IClassFixture<PlatformControllerFixture>, IDisposable
    {
        PlatformControllerFixture fixture;

        public PlatformControllerTests(PlatformControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockPlatformRepo.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsListOfPlatforms()
        {
            var platformList = fixture.fixture.Create<IEnumerable<Platform>>();

            fixture.mockPlatformRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(platformList);

            var result = await fixture.platformController.GetAll();

            fixture.mockPlatformRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(platformList, result.Data);
            Assert.True(result.NumberOfRows > 0);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsEmptyList()
        {
            fixture.mockPlatformRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Platform>());

            var result = await fixture.platformController.GetAll();

            fixture.mockPlatformRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(Enumerable.Empty<Platform>(), result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_SuccessResponse_ReturnsPlatform()
        {
            var platform = fixture.fixture.Create<Platform>();

            fixture.mockPlatformRepo.Setup(x => x.GetByIDAsync(platform.ID)).ReturnsAsync(platform);

            var result = await fixture.platformController.GetByID(platform.ID);

            fixture.mockPlatformRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<Platform>(result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenIDIs0()
        {
            fixture.mockPlatformRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Platform)null);

            var result = await fixture.platformController.GetByID(0);

            fixture.mockPlatformRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Insert
        [Fact]
        public async Task Insert_SuccessResponse_CreatesPlatform()
        {
            var platform = fixture.fixture.Create<Platform>();

            fixture.mockPlatformRepo.Setup(x => x.InsertAsync(platform)).ReturnsAsync(1);

            var result = await fixture.platformController.Insert(platform);

            fixture.mockPlatformRepo.Verify(x => x.InsertAsync(It.IsAny<Platform>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Insert_ErrorResponse_WhenPlatformIsNull()
        {
            fixture.mockPlatformRepo.Setup(x => x.InsertAsync(null)).ReturnsAsync(0);

            var result = await fixture.platformController.Insert(null);

            fixture.mockPlatformRepo.Verify(x => x.InsertAsync(It.IsAny<Platform>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_SuccessResponse_UpdatesPlatform()
        {
            var id = 1;
            var platform = new Platform
            {
                Name = "PS4"
            };
            var updatedPlatform = new Platform
            {
                ID = 1,
                Name = "XBOX Series X"
            };

            fixture.mockPlatformRepo.Setup(x => x.GetByIDAsync(id)).ReturnsAsync(updatedPlatform);
            fixture.mockPlatformRepo.Setup(x => x.UpdateAsync(updatedPlatform)).ReturnsAsync(1);

            var result = await fixture.platformController.Update(id, platform);

            fixture.mockPlatformRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPlatformRepo.Verify(x => x.UpdateAsync(updatedPlatform), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(updatedPlatform, result.Data);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenIDIs0()
        {
            var platform = fixture.fixture.Create<Platform>();

            fixture.mockPlatformRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Platform)null);

            var result = await fixture.platformController.Update(0, platform);

            fixture.mockPlatformRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockPlatformRepo.Verify(x => x.UpdateAsync(It.IsAny<Platform>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenPlatformIsNull()
        {
            var platform = fixture.fixture.Create<Platform>();

            fixture.mockPlatformRepo.Setup(x => x.GetByIDAsync(platform.ID)).ReturnsAsync(platform);
            fixture.mockPlatformRepo.Setup(x => x.UpdateAsync(null)).ReturnsAsync(0);

            var result = await fixture.platformController.Update(platform.ID, platform);

            fixture.mockPlatformRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPlatformRepo.Verify(x => x.UpdateAsync(It.IsAny<Platform>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_SuccessResponse_DeletesPlatform()
        {
            var platform = fixture.fixture.Create<Platform>();

            fixture.mockPlatformRepo.Setup(x => x.GetByIDAsync(platform.ID)).ReturnsAsync(platform);
            fixture.mockPlatformRepo.Setup(x => x.DeleteAsync(platform)).ReturnsAsync(1);

            var result = await fixture.platformController.Delete(platform.ID);

            fixture.mockPlatformRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPlatformRepo.Verify(x => x.DeleteAsync(It.IsAny<Platform>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenIDIs0()
        {
            fixture.mockPlatformRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Platform)null);

            var result = await fixture.platformController.Delete(0);

            fixture.mockPlatformRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockPlatformRepo.Verify(x => x.DeleteAsync(It.IsAny<Platform>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenPlatformIsNull()
        {
            var platform = fixture.fixture.Create<Platform>();

            fixture.mockPlatformRepo.Setup(x => x.GetByIDAsync(platform.ID)).ReturnsAsync(platform);
            fixture.mockPlatformRepo.Setup(x => x.DeleteAsync(null)).ReturnsAsync(0);

            var result = await fixture.platformController.Delete(platform.ID);

            fixture.mockPlatformRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPlatformRepo.Verify(x => x.DeleteAsync(It.IsAny<Platform>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion
    }
}
