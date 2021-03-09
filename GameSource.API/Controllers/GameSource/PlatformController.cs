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

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Platform list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Platform list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await platformService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Platform.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Platform.");
        }

        [HttpPost]
        public async Task Insert([FromBody] Platform platform)
        {
            await platformService.InsertAsync(platform);
        }

        [HttpPost]
        public async Task Update([FromBody] Platform platform)
        {
            await platformService.UpdateAsync(platform);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await platformService.DeleteAsync(id);
        }
    }
}