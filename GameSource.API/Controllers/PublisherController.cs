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
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Publisher> result = await publisherService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Publisher list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Publisher list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await publisherService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User.");
        }

        [HttpPost]
        public async Task Insert([FromBody] Publisher publisher)
        {
            await publisherService.InsertAsync(publisher);
        }

        [HttpPost]
        public async Task Update([FromBody] Publisher publisher)
        {
            await publisherService.UpdateAsync(publisher);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await publisherService.DeleteAsync(id);
        }
    }
}