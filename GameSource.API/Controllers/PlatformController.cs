using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.API.Controllers.GameSource
{
    [Route("api/platforms")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService platformService;

        public PlatformController(IPlatformService platformService)
        {
            this.platformService = platformService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Platform> result = await platformService.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Platform list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Platform list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await platformService.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Platform.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Platform.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Platform platform)
        {
            int rows = await platformService.InsertAsync(platform);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Platform.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Platform.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Platform platform)
        {
            int rows = await platformService.UpdateAsync(platform);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Platform.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Platform.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await platformService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Platform.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted Platform.");
        }
    }
}