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
    public class DeveloperControllerTests : IClassFixture<DeveloperControllerFixture>, IDisposable
    {
        DeveloperControllerFixture fixture;

        public DeveloperControllerTests(DeveloperControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockDeveloperRepo.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_ReturnsListOfDevelopers()
        {
            var developerList = fixture.fixture.Create<IEnumerable<Developer>>();

            fixture.mockDeveloperRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(developerList);

            var result = await fixture.developerController.GetAll();

            fixture.mockDeveloperRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(developerList, result.Data);
            Assert.True(result.NumberOfRows > 0);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_ReturnsEmptyList()
        {
            fixture.mockDeveloperRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Developer>());

            var result = await fixture.developerController.GetAll();

            fixture.mockDeveloperRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(Enumerable.Empty<Developer>(), result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_ReturnsDeveloper()
        {
            var developer = fixture.fixture.Create<Developer>();

            fixture.mockDeveloperRepo.Setup(x => x.GetByIDAsync(developer.ID)).ReturnsAsync(developer);

            var result = await fixture.developerController.GetByID(developer.ID);

            fixture.mockDeveloperRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<Developer>(result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_InvalidID_WhenIDIs0()
        {
            fixture.mockDeveloperRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Developer)null);

            var result = await fixture.developerController.GetByID(0);

            fixture.mockDeveloperRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Insert
        [Fact]
        public async Task Insert_CreatesDeveloper()
        {
            var developer = fixture.fixture.Create<Developer>();

            fixture.mockDeveloperRepo.Setup(x => x.InsertAsync(developer)).ReturnsAsync(1);

            var result = await fixture.developerController.Insert(developer);

            fixture.mockDeveloperRepo.Verify(x => x.InsertAsync(It.IsAny<Developer>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Insert_RequestFailed()
        {
            fixture.mockDeveloperRepo.Setup(x => x.InsertAsync(null)).ReturnsAsync(0);

            var result = await fixture.developerController.Insert(null);

            fixture.mockDeveloperRepo.Verify(x => x.InsertAsync(It.IsAny<Developer>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_UpdatesDeveloper()
        {
            var id = 1;
            var developer = new Developer
            {
                Name = "BioWare"
            };
            var updatedDeveloper = new Developer
            {
                ID = 1,
                Name = "Naughty Dog"
            };

            fixture.mockDeveloperRepo.Setup(x => x.GetByIDAsync(id)).ReturnsAsync(updatedDeveloper);
            fixture.mockDeveloperRepo.Setup(x => x.UpdateAsync(updatedDeveloper)).ReturnsAsync(1);

            var result = await fixture.developerController.Update(id, developer);

            fixture.mockDeveloperRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockDeveloperRepo.Verify(x => x.UpdateAsync(It.IsAny<Developer>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(updatedDeveloper, result.Data);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_InvalidID_WhenIDIs0()
        {
            var developer = new Developer
            {
                Name = "BioWare"
            };

            fixture.mockDeveloperRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Developer)null);

            var result = await fixture.developerController.Update(0, developer);

            fixture.mockDeveloperRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockDeveloperRepo.Verify(x => x.UpdateAsync(It.IsAny<Developer>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_RequestFailed()
        {
            var developer = new Developer
            {
                ID = 1,
                Name = "BioWare"
            };

            fixture.mockDeveloperRepo.Setup(x => x.GetByIDAsync(developer.ID)).ReturnsAsync(developer);
            fixture.mockDeveloperRepo.Setup(x => x.UpdateAsync(null)).ReturnsAsync(0);

            var result = await fixture.developerController.Update(developer.ID, developer);

            fixture.mockDeveloperRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockDeveloperRepo.Verify(x => x.UpdateAsync(It.IsAny<Developer>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_DeletesDeveloper()
        {
            var developer = new Developer
            {
                ID = 1,
                Name = "BioWare"
            };

            fixture.mockDeveloperRepo.Setup(x => x.GetByIDAsync(developer.ID)).ReturnsAsync(developer);
            fixture.mockDeveloperRepo.Setup(x => x.DeleteAsync(developer)).ReturnsAsync(1);

            var result = await fixture.developerController.Delete(developer.ID);

            fixture.mockDeveloperRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockDeveloperRepo.Verify(x => x.DeleteAsync(It.IsAny<Developer>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_InvalidID_WhenIDIs0()
        {
            fixture.mockDeveloperRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Developer)null);

            var result = await fixture.developerController.Delete(0);

            fixture.mockDeveloperRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockDeveloperRepo.Verify(x => x.DeleteAsync(It.IsAny<Developer>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_RequestFailed()
        {
            var developer = new Developer
            {
                ID = 1,
                Name = "BioWare"
            };

            fixture.mockDeveloperRepo.Setup(x => x.GetByIDAsync(developer.ID)).ReturnsAsync(developer);
            fixture.mockDeveloperRepo.Setup(x => x.DeleteAsync(null)).ReturnsAsync(0);

            var result = await fixture.developerController.Delete(developer.ID);

            fixture.mockDeveloperRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockDeveloperRepo.Verify(x => x.DeleteAsync(It.IsAny<Developer>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion
    }
}
