using System.Linq;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using GameSource.Models.DTOs.GameSource;
using System.Collections.Generic;

namespace GameSource.API.Controllers
{
    [Route("api/games")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository gameRepository;
        private readonly IPlatformRepository platformRepository;

        public GamesController(IGameRepository gameRepository, IPlatformRepository platformRepository)
        {
            this.gameRepository = gameRepository;
            this.platformRepository = platformRepository;
        }

        /// <summary>
        /// Gets all Games
        /// </summary>
        /// <response code="200">Returns a list of Games</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            var result = await gameRepository.GetAllAsync();

            var gameList = new List<GameDTO>();
            foreach (var game in result)
            {
                //Explicit loading
                await gameRepository.GetPlatformsAsync(game);
                await gameRepository.GetReviewsAsync(game);
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
                
                gameList.Add(dto);
            }

            return new ApiResponse(gameList, ResponseStatusCode.Success, "Successfully returned Game list.", gameList.Count());
        }

        /// <summary>
        /// Gets a Game by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a Game</response>
        /// <response code="404">Could not find a Game</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID was passed. Please check the ID.");

            var result = await gameRepository.GetByIDAsync(id);
            if (result == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Game was not found.");

            await gameRepository.GetPlatformsAsync(result);
            await gameRepository.GetReviewsAsync(result);
            var dto = new GameDTO
            {
                ID = result.ID,
                Name = result.Name,
                Description = result.Description,
                CoverImageFilePath = result.CoverImageFilePath,
                GenreID = result.GenreID,
                DeveloperID = result.DeveloperID,
                PublisherID = result.PublisherID,
                Platforms = result.Platforms.Select(x => new PlatformDTO
                {
                    ID = x.ID,
                    Name = x.Name,
                    PlatformTypeID = x.PlatformTypeID
                }),
                Reviews = result.Reviews.Select(x => new ReviewDTO
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

            return new ApiResponse(dto, ResponseStatusCode.Success, "Successfully returned a Game.", 1);
        }

        /// <summary>
        /// Creates a new Game
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Mass Effect"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new Game</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Game game)
        {
            var inserted = await gameRepository.InsertAsync(game);
            if (!inserted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a Game.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new Game.", 1);
        }

        /// <summary>
        /// Updates a Game
        /// </summary>
        /// <param name="id"></param>
        /// <param name="game"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Mass Effect"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a Game</response>
        /// <response code="404">Could not find a Game</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Game game)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID was passed. Please check the ID.");

            Game updatedGame = await gameRepository.GetByIDAsync(id);
            if (updatedGame == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Game was not found. Please check the ID.");

            Platform platform = await platformRepository.GetByIDAsync(game.PlatformID);
            if (platform == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Platform was not found. Please check the ID.");

            updatedGame.Name = game.Name;
            updatedGame.Description = game.Description;
            updatedGame.CoverImageFilePath = game.CoverImageFilePath;
            updatedGame.GenreID = game.GenreID;
            updatedGame.DeveloperID = game.DeveloperID;
            updatedGame.PublisherID = game.PublisherID;
            updatedGame.Platforms.ToList().Add(platform);

            var updated = await gameRepository.UpdateAsync(updatedGame);
            if (!updated)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update Game.", 0);

            var dto = new UpdateGameDTO
            {
                ID = updatedGame.ID,
                Name = updatedGame.Name,
                Description = updatedGame.Description,
                CoverImageFilePath = updatedGame.CoverImageFilePath,
                GenreID = updatedGame.GenreID,
                DeveloperID = updatedGame.DeveloperID,
                PublisherID = updatedGame.PublisherID,
                Platforms = updatedGame.Platforms.Select(x => new PlatformDTO
                {
                    ID = x.ID,
                    Name = x.Name,
                    PlatformTypeID = x.PlatformTypeID
                }),
                Reviews = updatedGame.Reviews.Select(x => new ReviewDTO
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

            return new ApiResponse(dto, ResponseStatusCode.Success, "Successfully updated Game.", 1);
        }

        /// <summary>
        /// Deletes a Game
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a Game</response>
        /// <response code="404">Could not find a Game</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID was passed. Please check the ID.");

            Game game = await gameRepository.GetByIDAsync(id);
            if (game == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Game was not found. Please check the ID.");

            var deleted = await gameRepository.DeleteAsync(game);
            if (!deleted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete Game.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted Game.", 1);
        }
    }
}
