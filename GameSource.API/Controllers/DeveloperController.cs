﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Infrastructure.Repositories.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers.GameSource
{
    [Route("api/developers")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperRepository developerRepository;

        public DeveloperController(IDeveloperRepository developerRepository)
        {
            this.developerRepository = developerRepository;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Developer> result = await developerRepository.GetAllAsync();
            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return Developer list.", result.Count());

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Developer list.", result.Count());
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await developerRepository.GetByIDAsync(id);
            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Developer.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Developer.", 1);
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Developer developer)
        {
            int rows = await developerRepository.InsertAsync(developer);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a Developer.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new Developer.", rows);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] Developer developer)
        {
            Developer updatedDeveloper = developerRepository.GetByID(id);
            if (updatedDeveloper == null)
                return new ApiResponse(ResponseStatusCode.Error, "Developer was not found. Please check the ID.");

            updatedDeveloper.Name = developer.Name;
            updatedDeveloper.Games = developer.Games;

            int rows = await developerRepository.UpdateAsync(updatedDeveloper);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update Developer.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully updated Developer.", rows);
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Developer was not found. Please check the ID.");

            int rows = await developerRepository.DeleteAsync(id);
            if (rows <= 0)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete Developer.", rows);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted Developer.", rows);
        }
    }
}