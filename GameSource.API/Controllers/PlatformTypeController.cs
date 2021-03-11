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
    [EnableCors("AllowOrigin")]
    public class PlatformTypeController : ControllerBase
    {
        private readonly IPlatformTypeRepository platformTypeRepository;

        public PlatformTypeController(IPlatformTypeRepository platformTypeRepository)
        {
            this.platformTypeRepository = platformTypeRepository;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<PlatformType> result = await platformTypeRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Platform Type list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Platform Type list.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] PlatformType platformType)
        {
            int rows = await platformTypeRepository.InsertAsync(platformType);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Platform Type.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Platform Type.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] PlatformType platformType)
        {
            int rows = await platformTypeRepository.UpdateAsync(platformType);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Platform Type.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Platform Type.");
        }

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