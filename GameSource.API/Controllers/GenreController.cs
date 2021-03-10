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
    [Route("api/genres")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
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
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Genre.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Genre.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Genre genre)
        {
            int rows = await genreService.InsertAsync(genre);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Genre.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Genre.");
        }

        [HttpPut]
        public async Task<ApiResponse> Update([FromBody] Genre genre)
        {
            int rows = await genreService.UpdateAsync(genre);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Genre.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Genre.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await genreService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted the Genre.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Genre.");
        }
    }
}