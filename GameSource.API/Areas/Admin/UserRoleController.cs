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

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return UserRole list.");

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

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a UserRole.");
        }

        /// <summary>
        /// Creates a new UserRole
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>UserRole
        /// <response code="200">Creates a new UserRole</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserRole userRole)
        {
            int rows = await userRoleRepository.InsertAsync(userRole);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a UserRole.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new UserRole.");
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
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a UserRole</response>
        /// <response code="404">Could not find a UserRole</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserRole userRole)
        {
            int rows = await userRoleRepository.UpdateAsync(userRole);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update UserRole.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated UserRole.");
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
            UserRole userRole = await userRoleRepository.GetByIDAsync(id);
            if (id == 0 || userRole == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "UserRole was not found. Please check the ID.");

            int rows = await userRoleRepository.DeleteAsync(userRole);
            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete UserRole.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted UserRole.");
        }
    }
}