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
    [Route("api/developers")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperService developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            this.developerService = developerService;
        }

        [HttpGet]
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
        public async Task<ApiResponse> Insert([FromBody] Developer developer)
        {
            int rows = await developerService.InsertAsync(developer);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Developer.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Developer.");
        }

        [HttpPut]
        public async Task<ApiResponse> Update([FromBody] Developer developer)
        {
            int rows = await developerService.UpdateAsync(developer);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Developer.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Developer.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await developerService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted Developer.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Developer.");
        }
    }
}