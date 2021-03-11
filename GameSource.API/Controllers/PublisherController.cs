﻿using System.Collections.Generic;
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
    [EnableCors("AllowOrigin")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository publisherRepository;

        public PublisherController(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Publisher> result = await publisherRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Publisher list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Publisher list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await publisherRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Publisher.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Publisher.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Publisher publisher)
        {
            int rows = await publisherRepository.InsertAsync(publisher);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a Publisher.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new Publisher.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Publisher publisher)
        {
            int rows = await publisherRepository.UpdateAsync(publisher);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update Publisher.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated Publisher.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await publisherRepository.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete Publisher.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted Publisher.");
        }
    }
}