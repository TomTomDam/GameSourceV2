using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Areas.Admin
{
    [Route("api/user-roles")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            this.userRoleService = userRoleService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserRole> result = await userRoleService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User Role list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User Role list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userRoleService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User Role.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User Role.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserRole userRole)
        {
            int rows = await userRoleService.InsertAsync(userRole);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new User Role.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a User Role.");
        }

        [HttpPut]
        public async Task<ApiResponse> Update([FromBody] UserRole userRole)
        {
            int rows = await userRoleService.UpdateAsync(userRole);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated User Role.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update User Role.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await userRoleService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted User Role.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete User Role.");
        }
    }
}