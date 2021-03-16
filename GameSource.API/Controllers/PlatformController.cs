using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GameSource.API.Controllers.GameSource
{
    [Route("api/platforms")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository platformRepository;

        public PlatformController(IPlatformRepository platformRepository)
        {
            this.platformRepository = platformRepository;
        }

        /// <summary>
        /// Gets all Platforms
        /// </summary>
        /// <response code="200">Returns a list of Platforms</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Platform> result = await platformRepository.GetAllAsync();

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Platform list.", result.Count());
        }

        /// <summary>
        /// Gets a Platform by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a Platform</response>
        /// <response code="404">Could not find a Platform</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var result = await platformRepository.GetByIDAsync(id);
            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Platform.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Platform.");
        }

        /// <summary>
        /// Creates a new Platform
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "PS4"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new Platform</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Platform platform)
        {
            int rows = await platformRepository.InsertAsync(platform);

            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a Platform.", rows);

            return new ApiResponse(platform, ResponseStatusCode.Success, "Successfully created a new Platform.", rows);
        }

        /// <summary>
        /// Updates a Platform
        /// </summary>
        /// <param name="id"></param>
        /// <param name="platform"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "PS4"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a Platform</response>
        /// <response code="404">Could not find a Platform</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Platform platform)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var updatedPlatform = await platformRepository.GetByIDAsync(id);
            if (updatedPlatform == null)
                return new ApiResponse(ResponseStatusCode.Error, "Platform was not found. Please check the ID.");

            updatedPlatform.Name = platform.Name;
            updatedPlatform.PlatformTypeID = platform.PlatformTypeID;

            int rows = await platformRepository.UpdateAsync(updatedPlatform);
            if (rows <= 0)
                return new ApiResponse(updatedPlatform, ResponseStatusCode.Error, "Could not update Platform.", rows);

            return new ApiResponse(updatedPlatform, ResponseStatusCode.Success, "Successfully updated Platform.", rows);
        }

        /// <summary>
        /// Deletes a Platform
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a Platform</response>
        /// <response code="404">Could not find a Platform</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            Platform platform = await platformRepository.GetByIDAsync(id);
            if (platform == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Platform was not found. Please check the ID.");

            int rows = await platformRepository.DeleteAsync(platform);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete Platform.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted Platform.", rows);
        }
    }
}