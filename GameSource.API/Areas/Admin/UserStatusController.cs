using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Areas.Admin
{
    [Route("api/admin/user-statuses")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class UserStatusController : ControllerBase
    {
        private readonly IUserStatusRepository userStatusRepository;

        public UserStatusController(IUserStatusRepository userStatusRepository)
        {
            this.userStatusRepository = userStatusRepository;
        }

        /// <summary>
        /// Gets all UserStatuses
        /// </summary>
        /// <response code="200">Returns a list of UserStatuses</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserStatus> result = await userStatusRepository.GetAllAsync();

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned UserStatus list.");
        }

        /// <summary>
        /// Gets a UserStatus by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a UserStatus</response>
        /// <response code="404">Could not find a UserStatus</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userStatusRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a UserStatus.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a UserStatus.", 1);
        }

        /// <summary>
        /// Creates a new UserStatus
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Active"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new UserStatus</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserStatus userStatus)
        {
            var inserted = await userStatusRepository.InsertAsync(userStatus);
            if (!inserted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a UserStatus.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new UserStatus.", 1);
        }

        /// <summary>
        /// Updates a UserStatus
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userStatus"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Active"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a UserStatus</response>
        /// <response code="404">Could not find a UserStatus</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserStatus userStatus)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var updatedUserStatus = await userStatusRepository.GetByIDAsync(id);
            if (updatedUserStatus == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "UserStatus was not found.");

            updatedUserStatus.Name = userStatus.Name;

            var updated = await userStatusRepository.UpdateAsync(userStatus);
            if (!updated)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update UserStatus.", 0);

            return new ApiResponse(updatedUserStatus, ResponseStatusCode.Success, "Successfully updated UserStatus.", 1);
        }

        /// <summary>
        /// Deletes a UserStatus
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a UserStatus</response>
        /// <response code="404">Could not find a UserStatus</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            UserStatus userStatus = await userStatusRepository.GetByIDAsync(id);
            if (userStatus == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "UserStatus was not found. Please check the ID.");

            var deleted = await userStatusRepository.DeleteAsync(userStatus);
            if (!deleted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete UserStatus.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted UserStatus.", 1);
        }
    }
}