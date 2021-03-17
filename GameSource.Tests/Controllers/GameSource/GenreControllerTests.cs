using AutoFixture;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Tests.Fixtures;
using GameSource.Tests.Fixtures.Controllers.GameSource;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GameSource.Tests.Controllers.GameSource
{
    public class GenreControllerTests : IClassFixture<GenreControllerFixture>, IDisposable
    {
        GenreControllerFixture fixture;

        public GenreControllerTests(GenreControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockGenreRepo.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsListOfGenres()
        {
            var genreList = fixture.fixture.Create<IEnumerable<Genre>>();

            fixture.mockGenreRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(genreList);

            var result = await fixture.genreController.GetAll();

            fixture.mockGenreRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(genreList, result.Data);
            Assert.True(result.NumberOfRows > 0);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsEmptyList()
        {
            fixture.mockGenreRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Genre>());

            var result = await fixture.genreController.GetAll();

            fixture.mockGenreRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(Enumerable.Empty<Genre>(), result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_SuccessResponse_ReturnsGenre()
        {
            var genre = fixture.fixture.Create<Genre>();

            fixture.mockGenreRepo.Setup(x => x.GetByIDAsync(genre.ID)).ReturnsAsync(genre);

            var result = await fixture.genreController.GetByID(genre.ID);

            fixture.mockGenreRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<Genre>(result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenIDIs0()
        {
            fixture.mockGenreRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Genre)null);

            var result = await fixture.genreController.GetByID(0);

            fixture.mockGenreRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenGenreIsNotFound()
        {
            fixture.mockGenreRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((Genre)null);

            var result = await fixture.genreController.GetByID(1);

            fixture.mockGenreRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }
        #endregion

        #region Insert
        [Fact]
        public async Task Insert_SuccessResponse_CreatesGenre()
        {
            var genre = fixture.fixture.Create<Genre>();

            fixture.mockGenreRepo.Setup(x => x.InsertAsync(genre)).ReturnsAsync(1);

            var result = await fixture.genreController.Insert(genre);

            fixture.mockGenreRepo.Verify(x => x.InsertAsync(It.IsAny<Genre>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Insert_ErrorResponse_WhenGenreIsNull()
        {
            fixture.mockGenreRepo.Setup(x => x.InsertAsync(null)).ReturnsAsync(0);

            var result = await fixture.genreController.Insert(null);

            fixture.mockGenreRepo.Verify(x => x.InsertAsync(It.IsAny<Genre>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_SuccessResponse_UpdatesGenre()
        {
            var id = 1;
            var genre = new Genre
            {
                Name = "Sci-Fi"
            };
            var updatedGenre = new Genre
            {
                ID = 1,
                Name = "Horror"
            };

            fixture.mockGenreRepo.Setup(x => x.GetByIDAsync(id)).ReturnsAsync(updatedGenre);
            fixture.mockGenreRepo.Setup(x => x.UpdateAsync(updatedGenre)).ReturnsAsync(1);

            var result = await fixture.genreController.Update(id, genre);

            fixture.mockGenreRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockGenreRepo.Verify(x => x.UpdateAsync(updatedGenre), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(updatedGenre, result.Data);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenIDIs0()
        {
            var genre = fixture.fixture.Create<Genre>();

            fixture.mockGenreRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Genre)null);

            var result = await fixture.genreController.Update(0, genre);

            fixture.mockGenreRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockGenreRepo.Verify(x => x.UpdateAsync(It.IsAny<Genre>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenGenreIsNull()
        {
            var genre = fixture.fixture.Create<Genre>();

            fixture.mockGenreRepo.Setup(x => x.GetByIDAsync(genre.ID)).ReturnsAsync(genre);
            fixture.mockGenreRepo.Setup(x => x.UpdateAsync(null)).ReturnsAsync(0);

            var result = await fixture.genreController.Update(genre.ID, genre);

            fixture.mockGenreRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockGenreRepo.Verify(x => x.UpdateAsync(It.IsAny<Genre>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_SuccessResponse_DeletesGenre()
        {
            var genre = fixture.fixture.Create<Genre>();

            fixture.mockGenreRepo.Setup(x => x.GetByIDAsync(genre.ID)).ReturnsAsync(genre);
            fixture.mockGenreRepo.Setup(x => x.DeleteAsync(genre)).ReturnsAsync(1);

            var result = await fixture.genreController.Delete(genre.ID);

            fixture.mockGenreRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockGenreRepo.Verify(x => x.DeleteAsync(It.IsAny<Genre>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenIDIs0()
        {
            fixture.mockGenreRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Genre)null);

            var result = await fixture.genreController.Delete(0);

            fixture.mockGenreRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockGenreRepo.Verify(x => x.DeleteAsync(It.IsAny<Genre>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenGenreIsNull()
        {
            var genre = fixture.fixture.Create<Genre>();

            fixture.mockGenreRepo.Setup(x => x.GetByIDAsync(genre.ID)).ReturnsAsync(genre);
            fixture.mockGenreRepo.Setup(x => x.DeleteAsync(null)).ReturnsAsync(0);

            var result = await fixture.genreController.Delete(genre.ID);

            fixture.mockGenreRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockGenreRepo.Verify(x => x.DeleteAsync(It.IsAny<Genre>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion
    }
}
