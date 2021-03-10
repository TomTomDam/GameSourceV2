using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers
{
    [Route("api/games")]
    [ApiController]
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
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User.");
        }

        [HttpPost]
        public async Task Insert([FromBody] Game game)
        {
            await gameService.InsertAsync(game);
        }

        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] Game game)
        {
            await gameService.UpdateAsync(game);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await gameService.DeleteAsync(id);
        }
    }
}
