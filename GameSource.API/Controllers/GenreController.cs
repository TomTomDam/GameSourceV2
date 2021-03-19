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
    [Route("api/genres")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        /// <summary>
        /// Gets all Genres
        /// </summary>
        /// <response code="200">Returns a list of Genres</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Genre> result = await genreRepository.GetAllAsync();

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Genre list.", result.Count());
        }

        /// <summary>
        /// Gets a Genre by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a Genre</response>
        /// <response code="404">Could not find a Genre</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID was passed. Please check the ID.");

            var result = await genreRepository.GetByIDAsync(id);
            if (result == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Could not return a Genre.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Genre.", 1);
        }

        /// <summary>
        /// Creates a new Genre
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Sci-Fi"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new Genre</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Genre genre)
        {
            var inserted = await genreRepository.InsertAsync(genre);
            if (!inserted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a Genre.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a Genre.", 1);
        }

        /// <summary>
        /// Updates a Genre
        /// </summary>
        /// <param name="id"></param>
        /// <param name="genre"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Sci-Fi"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a Genre</response>
        /// <response code="404">Could not find a Genre</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Genre genre)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID was passed. Please check the ID.");

            Genre updatedGenre = await genreRepository.GetByIDAsync(id);
            if (updatedGenre == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Genre was not found. Please check the ID.");

            updatedGenre.Name = genre.Name;

            var updated = await genreRepository.UpdateAsync(updatedGenre);
            if (!updated)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update Genre.", 0);

            return new ApiResponse(updatedGenre, ResponseStatusCode.Success, "Successfully updated Genre.", 1);
        }

        /// <summary>
        /// Deletes a Genre
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a Genre</response>
        /// <response code="404">Could not find a Genre</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID was passed. Please check the ID.");

            Genre genre = await genreRepository.GetByIDAsync(id);
            if (genre == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Genre was not found. Please check the ID.");

            var deleted = await genreRepository.DeleteAsync(genre);
            if (!deleted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete Genre.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted a Genre.", 1);
        }
    }
}