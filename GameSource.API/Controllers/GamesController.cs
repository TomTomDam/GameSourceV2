using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers
{
    [Route("api/games")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository gameRepository;

        public GamesController(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Game> result = await gameRepository.GetAllAsync();
            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Game list.", result.Count());

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Game list.", result.Count());
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await gameRepository.GetByIDAsync(id);
            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Game.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Game.", 1);
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Game game)
        {
            int rows = await gameRepository.InsertAsync(game);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a Game.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new Game.", rows);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Game game)
        {
            Game updatedGame = gameRepository.GetByID(id);
            if (updatedGame == null)
                return new ApiResponse(ResponseStatusCode.Error, "Game was not found. Please check the ID.");

            updatedGame.Name = game.Name;
            updatedGame.Description = game.Description;
            updatedGame.CoverImageFilePath = game.CoverImageFilePath;
            updatedGame.GenreID = game.GenreID;
            updatedGame.DeveloperID = game.DeveloperID;
            updatedGame.PublisherID = game.PublisherID;
            updatedGame.PlatformID = game.PlatformID;

            int rows = await gameRepository.UpdateAsync(updatedGame);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update Game.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully updated Game.", rows);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Game was not found. Please check the ID.");

            int rows = await gameRepository.DeleteAsync(id);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete Game.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted Game.", rows);
        }
    }
}
