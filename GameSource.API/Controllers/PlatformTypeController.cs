using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GameSource.API.Controllers
{
    [Route("api/platorm-types")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class PlatformTypeController : ControllerBase
    {
        private readonly IPlatformTypeRepository platformTypeRepository;

        public PlatformTypeController(IPlatformTypeRepository platformTypeRepository)
        {
            this.platformTypeRepository = platformTypeRepository;
        }

        /// <summary>
        /// Gets all PlatformTypes
        /// </summary>
        /// <response code="200">Returns a list of PlatformTypes</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<PlatformType> result = await platformTypeRepository.GetAllAsync();

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned PlatformType list.", result.Count());
        }

        /// <summary>
        /// Gets a PlatformType by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a PlatformType</response>
        /// <response code="404">Could not find a PlatformType</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var result = await platformTypeRepository.GetByIDAsync(id);
            if (result == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "PlatformType was not found.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a PlatformType.");
        }

        /// <summary>
        /// Creates a new PlatformType
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Console"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new PlatformType</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] PlatformType platformType)
        {
            int rows = await platformTypeRepository.InsertAsync(platformType);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a PlatformType.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new PlatformType.", rows);
        }

        /// <summary>
        /// Updates a PlatformType
        /// </summary>
        /// <param name="id"></param>
        /// <param name="platformType"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Console"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a PlatformType</response>
        /// <response code="404">Could not find a PlatformType</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] PlatformType platformType)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var updatedPlatformType = await platformTypeRepository.GetByIDAsync(id);
            if (updatedPlatformType == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "PlatformType was not found.");

            updatedPlatformType.Name = platformType.Name;

            int rows = await platformTypeRepository.UpdateAsync(updatedPlatformType);
            if (rows <= 0)
                return new ApiResponse(updatedPlatformType, ResponseStatusCode.Error, "Could not update PlatformType.", rows);

            return new ApiResponse(updatedPlatformType, ResponseStatusCode.Success, "Successfully updated PlatformType.", rows);
        }

        /// <summary>
        /// Deletes a PlatformType
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a PlatformType</response>
        /// <response code="404">Could not find a PlatformType</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            PlatformType platformType = await platformTypeRepository.GetByIDAsync(id);
            if (platformType == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "PlatformType was not found. Please check the ID.");

            int rows = await platformTypeRepository.DeleteAsync(platformType);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete PlatformType.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted PlatformType.", rows);
        }
    }
}