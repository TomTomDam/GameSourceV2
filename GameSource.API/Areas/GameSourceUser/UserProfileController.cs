﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Areas.GameSourceUser
{
    [Route("api/user-profiles")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        /// <summary>
        /// Gets all UserProfiles
        /// </summary>
        /// <response code="200">Returns a list of UserProfiles</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserProfile> result = await userProfileRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User Profile list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User Profile list.");
        }

        /// <summary>
        /// Gets a UserProfile by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a UserProfile</response>
        /// <response code="404">Could not find a UserProfile</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userProfileRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User Profile.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User Profile.");
        }

        /// <summary>
        /// Creates a new UserProfile
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new UserProfile</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserProfile userProfile)
        {
            int rows = await userProfileRepository.InsertAsync(userProfile);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a User Profile.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new User Profile.");
        }

        /// <summary>
        /// Updates a UserProfile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userProfile"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a UserProfile</response>
        /// <response code="404">Could not find a UserProfile</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserProfile userProfile)
        {
            int rows = await userProfileRepository.UpdateAsync(userProfile);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update User Profile.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated User Profile.");
        }

        /// <summary>
        /// Deletes a UserProfile
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a UserProfile</response>
        /// <response code="404">Could not find a UserProfile</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await userProfileRepository.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete User Profile.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted User Profile.");
        }
    }
}