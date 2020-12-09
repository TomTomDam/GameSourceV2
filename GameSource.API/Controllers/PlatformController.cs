using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameSource.API.Controllers
{
    [Route("api/platform")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService platformService;

        public PlatformController(IPlatformService platformService)
        {
            this.platformService = platformService;
        }

        [HttpGet("GetAllAsync")]
        public async Task<ApiResponse> GetAllAsync()
        {
            IEnumerable<Platform> result = await platformService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Platform list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Platform list.");
        }
    }
}