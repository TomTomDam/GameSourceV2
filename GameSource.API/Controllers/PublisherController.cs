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

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Publisher list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Publisher list.");
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
            var result = await publisherRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Publisher.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Publisher.");
        }

        /// <summary>
        /// Creates a new Publisher
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new Publisher</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Publisher publisher)
        {
            int rows = await publisherRepository.InsertAsync(publisher);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Publisher.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Publisher.");
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
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a Publisher</response>
        /// <response code="404">Could not find a Publisher</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Publisher publisher)
        {
            int rows = await publisherRepository.UpdateAsync(publisher);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Publisher.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Publisher.");
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
            Publisher publisher = await publisherRepository.GetByIDAsync(id);
            if (id == 0 || publisher == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Publisher was not found. Please check the ID.");

            int rows = await publisherRepository.DeleteAsync(publisher);
            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Publisher.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted Publisher.");
        }
    }
}