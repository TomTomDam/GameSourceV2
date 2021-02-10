using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers.GameSource
{
    [Route("api/genre")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Genre> result = await genreService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Genre list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Genre list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await genreService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User.");
        }

        [HttpPost]
        public async Task Insert([FromBody] Genre genre)
        {
            await genreService.InsertAsync(genre);
        }

        [HttpPost]
        public async Task Update([FromBody] Genre genre)
        {
            await genreService.UpdateAsync(genre);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await genreService.DeleteAsync(id);
        }
    }
}