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
    [Route("api/admin/user-roles")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository userRoleRepository;

        public UserRoleController(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// Gets all UserRoles
        /// </summary>
        /// <response code="200">Returns a list of UserRole</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserRole> result = await userRoleRepository.GetAllAsync();

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned UserRole list.");
        }

        /// <summary>
        /// Gets a UserRole by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a UserRole</response>
        /// <response code="404">Could not find a UserRole</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userRoleRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a UserRole.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a UserRole.", 1);
        }

        /// <summary>
        /// Creates a new UserRole
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Member",
        ///         "description": "Default role for Users."
        ///     }
        ///     
        /// </remarks>UserRole
        /// <response code="200">Creates a new UserRole</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserRole userRole)
        {
            var inserted = await userRoleRepository.InsertAsync(userRole);
            if (!inserted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a UserRole.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new UserRole.", 1);
        }

        /// <summary>
        /// Updates a UserRole
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRole"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Member",
        ///         "description": "Default role for Users."
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a UserRole</response>
        /// <response code="404">Could not find a UserRole</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserRole userRole)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var updatedUserRole = await userRoleRepository.GetByIDAsync(id);
            if (updatedUserRole == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "UserRole was not found.");

            updatedUserRole.Name = userRole.Name;
            updatedUserRole.Description = userRole.Description;

            var updated = await userRoleRepository.UpdateAsync(userRole);
            if (!updated)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update UserRole.", 0);

            return new ApiResponse(updatedUserRole, ResponseStatusCode.Success, "Successfully updated UserRole.", 1);
        }

        /// <summary>
        /// Deletes a UserRole
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a UserRole</response>
        /// <response code="404">Could not find a UserRole</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            UserRole userRole = await userRoleRepository.GetByIDAsync(id);
            if (userRole == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "UserRole was not found. Please check the ID.");

            var deleted = await userRoleRepository.DeleteAsync(userRole);
            if (!deleted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete UserRole.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted UserRole.", 1);
        }
    }
}