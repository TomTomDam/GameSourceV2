using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers.GameSource
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

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Platform Type list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Platform Type list.");
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
            var result = await platformTypeRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Platform.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Platform.");
        }

        /// <summary>
        /// Creates a new PlatformType
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "BioWare"
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
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Platform Type.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Platform Type.");
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
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a PlatformType</response>
        /// <response code="404">Could not find a PlatformType</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] PlatformType platformType)
        {
            int rows = await platformTypeRepository.UpdateAsync(platformType);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Platform Type.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Platform Type.");
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
            int rows = await platformTypeRepository.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Platform Type.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted Platform Type.");
        }
    }
}