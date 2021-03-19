using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GameSource.API.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository publisherRepository;

        public PublisherController(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }

        /// <summary>
        /// Gets all Publishers
        /// </summary>
        /// <response code="200">Returns a list of Publishers</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Publisher> result = await publisherRepository.GetAllAsync();

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Publisher list.", result.Count());
        }

        /// <summary>
        /// Gets a Publisher by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a Publisher</response>
        /// <response code="404">Could not find a Publisher</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not return a Publisher.");

            var result = await publisherRepository.GetByIDAsync(id);
            if (result == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Publisher was not found.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Publisher.", 1);
        }

        /// <summary>
        /// Creates a new Publisher
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Electronic Arts"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new Publisher</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Publisher publisher)
        {
            var inserted = await publisherRepository.InsertAsync(publisher);
            if (!inserted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a Publisher.", 0);

            return new ApiResponse(publisher, ResponseStatusCode.Success, "Successfully created a new Publisher.", 1);
        }

        /// <summary>
        /// Updates a Publisher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publisher"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Electronic Arts"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a Publisher</response>
        /// <response code="404">Could not find a Publisher</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Publisher publisher)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var updatedPublisher = await publisherRepository.GetByIDAsync(id);
            if (updatedPublisher == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Publisher was not found.");

            updatedPublisher.Name = publisher.Name;

            var updated = await publisherRepository.UpdateAsync(updatedPublisher);
            if (!updated)
                return new ApiResponse(updatedPublisher, ResponseStatusCode.Error, "Could not update Publisher.", 0);

            return new ApiResponse(updatedPublisher, ResponseStatusCode.Success, "Successfully updated Publisher.", 1);
        }

        /// <summary>
        /// Deletes a Publisher
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a Publisher</response>
        /// <response code="404">Could not find a Publisher</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            Publisher publisher = await publisherRepository.GetByIDAsync(id);
            if (publisher == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Publisher was not found. Please check the ID.");

            var deleted = await publisherRepository.DeleteAsync(publisher);
            if (!deleted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete Publisher.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted Publisher.", 1);
        }
    }
}