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
    [Route("api/developers")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperRepository developerRepository;

        public DeveloperController(IDeveloperRepository developerRepository)
        {
            this.developerRepository = developerRepository;
        }

        /// <summary>
        /// Gets all Developers
        /// </summary>
        /// <response code="200">Returns a list of Developers</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Developer> result = await developerRepository.GetAllAsync();

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Developer list.", result.Count());
        }

        /// <summary>
        /// Gets a Developer by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a Developer</response>
        /// <response code="404">Could not find a Developer</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID was passed. Please check the ID.");

            var result = await developerRepository.GetByIDAsync(id);
            if (result == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Developer was not found.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Developer.", 1);
        }

        /// <summary>
        /// Creates a new Developer
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new Developer</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Developer developer)
        {
            int rows = await developerRepository.InsertAsync(developer);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a Developer.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new Developer.", rows);
        }

        /// <summary>
        /// Updates a Developer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="developer"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a Developer</response>
        /// <response code="404">Could not find a Developer</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Developer developer)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID was passed. Please check the ID.");

            Developer updatedDeveloper = await developerRepository.GetByIDAsync(id);
            if (updatedDeveloper == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Developer was not found.");

            updatedDeveloper.Name = developer.Name;
            updatedDeveloper.Games = developer.Games;

            int rows = await developerRepository.UpdateAsync(updatedDeveloper);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update Developer.", rows);

            return new ApiResponse(updatedDeveloper, ResponseStatusCode.Success, "Successfully updated Developer.", rows);
        }

        /// <summary>
        /// Deletes a Developer
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a Developer</response>
        /// <response code="404">Could not find a Developer</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID was passed. Please check the ID.");

            Developer developer = await developerRepository.GetByIDAsync(id);
            if (developer == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Developer was not found. Please check the ID.");

            int rows = await developerRepository.DeleteAsync(developer);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete Developer.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted Developer.", rows);
        }
    }
}