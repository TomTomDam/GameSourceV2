using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Developer list.", result.Count());

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Developer list.", result.Count());
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await developerService.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Developer.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Developer.", 1);
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Developer developer)
        {
            int rows = await developerService.InsertAsync(developer);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Developer.", rows);

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Developer.", rows);
        }

        [HttpPut]
        public async Task<ApiResponse> Update([FromBody] Developer developer)
        {
            int rows = await developerService.UpdateAsync(developer);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Developer.", rows);

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Developer.", rows);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await developerService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Developer.", rows);

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted Developer.", rows);
        }
    }
}