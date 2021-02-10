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
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class PlatformTypeController : ControllerBase
    {
        private readonly IPlatformTypeService platformTypeService;

        public PlatformTypeController(IPlatformTypeService platformTypeService)
        {
            this.platformTypeService = platformTypeService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<PlatformType> result = await platformTypeService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Platform Type list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Platform Type list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await platformTypeService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Platform Type.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Platform Type.");
        }

        [HttpPost]
        public async Task Insert([FromBody] PlatformType platformType)
        {
            await platformTypeService.InsertAsync(platformType);
        }

        [HttpPost]
        public async Task Update([FromBody] PlatformType platformType)
        {
            await platformTypeService.UpdateAsync(platformType);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await platformTypeService.DeleteAsync(id);
        }
    }
}