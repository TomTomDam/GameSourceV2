using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers
{
    [Route("api/games")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Game> games = await gameService.GetAllAsync();

            if (games != null)
                return new ApiResponse(games, ResponseStatusCode.Success, "Successfully returned Game list.");

            return new ApiResponse(games, ResponseStatusCode.Error, "Could not return Game list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await gameService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Game.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Game.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Game game)
        {
            int rows = await gameService.InsertAsync(game);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Game.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Game.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update([FromBody] Game game)
        {
            int rows = await gameService.UpdateAsync(game);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Game.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Game.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await gameService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted Game.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Game.");
        }
    }
}
