using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            this.developerService = developerService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Developer> result = await developerService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Developer list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Developer list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await developerService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Developer.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Developer.");
        }

        [HttpPost]
        public async Task Insert([FromBody] Developer developer)
        {
            await developerService.InsertAsync(developer);
        }

        [HttpPost]
        public async Task Update([FromBody] Developer developer)
        {
            await developerService.UpdateAsync(developer);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await developerService.DeleteAsync(id);
        }
    }
}