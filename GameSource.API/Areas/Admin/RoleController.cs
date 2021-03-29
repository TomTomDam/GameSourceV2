using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameSource.API.Areas.Admin
{
    [Route("api/admin/user-roles")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        /// <summary>
        /// Gets all Roles
        /// </summary>
        /// <response code="200">Returns a list of Roles</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<Role> result = await roleRepository.GetAllAsync();

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned Role list.");
        }

        /// <summary>
        /// Gets a Role by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a Role</response>
        /// <response code="404">Could not find a Role</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(Guid id)
        {
            var result = await roleRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a Role.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a Role.", 1);
        }

        /// <summary>
        /// Creates a new Role
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Member",
        ///         "description": "Default role for Users."
        ///     }
        ///     
        /// </remarks>Role
        /// <response code="200">Creates a new Role</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] Role Role)
        {
            var inserted = await roleRepository.InsertAsync(Role);
            if (!inserted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a Role.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new Role.", 1);
        }

        /// <summary>
        /// Updates a Role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Role"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "Member",
        ///         "description": "Default role for Users."
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a Role</response>
        /// <response code="404">Could not find a Role</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(Guid id, [FromBody] Role Role)
        {
            if (id == Guid.Empty)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var updatedRole = await roleRepository.GetByIDAsync(id);
            if (updatedRole == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Role was not found.");

            updatedRole.Name = Role.Name;
            updatedRole.Description = Role.Description;

            var updated = await roleRepository.UpdateAsync(Role);
            if (!updated)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update Role.", 0);

            return new ApiResponse(updatedRole, ResponseStatusCode.Success, "Successfully updated Role.", 1);
        }

        /// <summary>
        /// Deletes a Role
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a Role</response>
        /// <response code="404">Could not find a Role</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            Role Role = await roleRepository.GetByIDAsync(id);
            if (Role == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "Role was not found. Please check the ID.");

            var deleted = await roleRepository.DeleteAsync(Role);
            if (!deleted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete Role.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted Role.", 1);
        }
    }
}