using AutoFixture;
using FluentAssertions;
using GameSource.Models;
using GameSource.Models.DTOs.GameSource;
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
    public class GamesControllerTests : IClassFixture<GameControllerFixture>, IDisposable
    {
        GameControllerFixture fixture;

        public GamesControllerTests(GameControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        public void Dispose()
        {
            fixture.mockGameRepo.Invocations.Clear();
            fixture.mockPlatformRepo.Invocations.Clear();
        }

        #region GetAll
        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsListOfGames()
        {
            var gameList = fixture.fixture.CreateMany<Game>(2);
            var platforms = fixture.fixture.CreateMany<Platform>(1);
            var reviews = fixture.fixture.CreateMany<Review>(1);
            var gameDtoList = new List<GameDTO>();

            foreach (var game in gameList)
            {
                fixture.mockGameRepo.Setup(x => x.GetPlatformsAsync(game)).ReturnsAsync(platforms);
                fixture.mockGameRepo.Setup(x => x.GetReviewsAsync(game)).ReturnsAsync(reviews);

                var dto = new GameDTO
                {
                    ID = game.ID,
                    Name = game.Name,
                    Description = game.Description,
                    CoverImageFilePath = game.CoverImageFilePath,
                    GenreID = game.GenreID,
                    DeveloperID = game.DeveloperID,
                    PublisherID = game.PublisherID,
                    Platforms = game.Platforms.Select(x => new PlatformDTO
                    {
                        ID = x.ID,
                        Name = x.Name,
                        PlatformTypeID = x.PlatformTypeID
                    }),
                    Reviews = game.Reviews.Select(x => new ReviewDTO
                    {
                        ID = x.ID,
                        Title = x.Title,
                        Body = x.Body,
                        DateCreated = x.DateCreated,
                        DateModified = x.DateModified,
                        CreatedByID = x.CreatedByID,
                        HelpfulRating = x.HelpfulRating,
                        Rating = x.Rating,
                        ReviewComments = x.ReviewComments
                    })
                };
                
                gameDtoList.Add(dto);
            }

            fixture.mockGameRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(gameList);

            var result = await fixture.gameController.GetAll();

            fixture.mockGameRepo.Verify(x => x.GetAllAsync(), Times.Once);
            fixture.mockGameRepo.Verify(x => x.GetPlatformsAsync(It.IsAny<Game>()), Times.Exactly(2));
            fixture.mockGameRepo.Verify(x => x.GetReviewsAsync(It.IsAny<Game>()), Times.Exactly(2));

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<List<GameDTO>>(result.Data);
            result.Data.Should().BeEquivalentTo(gameDtoList);
            Assert.True(result.NumberOfRows > 0);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetAll_SuccessResponse_ReturnsEmptyList()
        {
            fixture.mockGameRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<Game>());

            var result = await fixture.gameController.GetAll();

            fixture.mockGameRepo.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(Enumerable.Empty<GameDTO>(), result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }
        #endregion

        #region GetByID
        [Fact]
        public async Task GetByID_SuccessResponse_ReturnsGame()
        {
            var game = fixture.fixture.Create<Game>();

            fixture.mockGameRepo.Setup(x => x.GetByIDAsync(game.ID)).ReturnsAsync(game);

            var result = await fixture.gameController.GetByID(game.ID);

            fixture.mockGameRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<GameDTO>(result.Data);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenIDIs0()
        {
            fixture.mockGameRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Game)null);

            var result = await fixture.gameController.GetByID(0);

            fixture.mockGameRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task GetByID_ErrorResponse_WhenGameIsNotFound()
        {
            fixture.mockGameRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((Game)null);

            var result = await fixture.gameController.GetByID(1);

            fixture.mockGameRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }
        #endregion

        #region Insert
        [Fact]
        public async Task Insert_SuccessResponse_CreatesGame()
        {
            var game = fixture.fixture.Create<Game>();

            fixture.mockGameRepo.Setup(x => x.InsertAsync(game)).ReturnsAsync(true);

            var result = await fixture.gameController.Insert(game);

            fixture.mockGameRepo.Verify(x => x.InsertAsync(It.IsAny<Game>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Insert_ErrorResponse_WhenGameIsNull()
        {
            fixture.mockGameRepo.Setup(x => x.InsertAsync(null)).ReturnsAsync(false);

            var result = await fixture.gameController.Insert(null);

            fixture.mockGameRepo.Verify(x => x.InsertAsync(It.IsAny<Game>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_SuccessResponse_UpdatesGame()
        {
            var id = 1;
            var platform = new Platform
            {
                ID = 1,
                Name = "Windows"
            };
            var game = new Game
            {
                Name = "Mass Effect",
                Description = "Shepard.",
                CoverImageFilePath =  "masseffect",
                GenreID = 2,
                DeveloperID = 1,
                PublisherID = 2,
                PlatformID = 1
            };
            var updatedGame = new Game
            {
                ID = 1,
                Name = "Star Wars: Knights of the Old Republic",
                Description = "Mucha shaka paka",
                CoverImageFilePath = "kotor",
                GenreID = 2,
                DeveloperID = 1,
                PublisherID = 2
            };
            var dto = new UpdateGameDTO
            {
                ID = id,
                Name = game.Name,
                Description = game.Description,
                CoverImageFilePath = game.CoverImageFilePath,
                GenreID = game.GenreID,
                DeveloperID = game.DeveloperID,
                PublisherID = game.PublisherID,
                Platforms = game.Platforms.Select(x => new PlatformDTO
                {
                    ID = x.ID,
                    Name = x.Name,
                    PlatformTypeID = x.PlatformTypeID
                }),
                Reviews = game.Reviews.Select(x => new ReviewDTO
                {
                    ID = x.ID,
                    Title = x.Title,
                    Body = x.Body,
                    DateCreated = x.DateCreated,
                    DateModified = x.DateModified,
                    CreatedByID = x.CreatedByID,
                    HelpfulRating = x.HelpfulRating,
                    Rating = x.Rating,
                    ReviewComments = x.ReviewComments
                })
            };

            fixture.mockGameRepo.Setup(x => x.GetByIDAsync(id)).ReturnsAsync(updatedGame);
            fixture.mockPlatformRepo.Setup(x => x.GetByIDAsync(platform.ID)).ReturnsAsync(platform);
            fixture.mockGameRepo.Setup(x => x.UpdateAsync(updatedGame)).ReturnsAsync(true);

            var result = await fixture.gameController.Update(id, game);

            fixture.mockGameRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockPlatformRepo.Verify(x => x.GetByIDAsync(platform.ID), Times.Once);
            fixture.mockGameRepo.Verify(x => x.UpdateAsync(updatedGame), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.IsType<UpdateGameDTO>(result.Data);
            result.Data.Should().BeEquivalentTo(dto);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenIDIs0()
        {
            var game = fixture.fixture.Create<Game>();

            fixture.mockGameRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Game)null);

            var result = await fixture.gameController.Update(0, game);

            fixture.mockGameRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockGameRepo.Verify(x => x.UpdateAsync(It.IsAny<Game>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Update_ErrorResponse_WhenGameIsNotFound()
        {
            var game = fixture.fixture.Create<Game>();

            fixture.mockGameRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((Game)null);

            var result = await fixture.gameController.Update(game.ID, game);

            fixture.mockGameRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockGameRepo.Verify(x => x.UpdateAsync(It.IsAny<Game>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_SuccessResponse_DeletesGame()
        {
            var game = fixture.fixture.Create<Game>();

            fixture.mockGameRepo.Setup(x => x.GetByIDAsync(game.ID)).ReturnsAsync(game);
            fixture.mockGameRepo.Setup(x => x.DeleteAsync(game)).ReturnsAsync(true);

            var result = await fixture.gameController.Delete(game.ID);

            fixture.mockGameRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockGameRepo.Verify(x => x.DeleteAsync(It.IsAny<Game>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(1, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Success, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenIDIs0()
        {
            fixture.mockGameRepo.Setup(x => x.GetByIDAsync(0)).ReturnsAsync((Game)null);

            var result = await fixture.gameController.Delete(0);

            fixture.mockGameRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Never);
            fixture.mockGameRepo.Verify(x => x.DeleteAsync(It.IsAny<Game>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenGameIsNotFound()
        {
            var game = fixture.fixture.Create<Game>();

            fixture.mockGameRepo.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync((Game)null);

            var result = await fixture.gameController.Delete(game.ID);

            fixture.mockGameRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockGameRepo.Verify(x => x.DeleteAsync(It.IsAny<Game>()), Times.Never);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(ResponseStatusCode.NotFound, result.ResponseStatusCode);
        }

        [Fact]
        public async Task Delete_ErrorResponse_WhenGameIsNotDeleted()
        {
            var game = fixture.fixture.Create<Game>();

            fixture.mockGameRepo.Setup(x => x.GetByIDAsync(game.ID)).ReturnsAsync(game);
            fixture.mockGameRepo.Setup(x => x.DeleteAsync(game)).ReturnsAsync(false);

            var result = await fixture.gameController.Delete(game.ID);

            fixture.mockGameRepo.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once);
            fixture.mockGameRepo.Verify(x => x.DeleteAsync(It.IsAny<Game>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<ApiResponse>(result);
            Assert.Null(result.Data);
            Assert.Equal(0, result.NumberOfRows);
            Assert.Equal(ResponseStatusCode.Error, result.ResponseStatusCode);
        }
        #endregion
    }
}
