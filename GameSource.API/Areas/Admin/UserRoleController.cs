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
    [EnableCors("AllowOrigin")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository userRoleRepository;

        public UserRoleController(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserRole> result = await userRoleRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User Role list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User Role list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userRoleRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User Role.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User Role.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserRole userRole)
        {
            int rows = await userRoleRepository.InsertAsync(userRole);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a User Role.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new User Role.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserRole userRole)
        {
            int rows = await userRoleRepository.UpdateAsync(userRole);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update User Role.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated User Role.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await userRoleRepository.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete User Role.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted User Role.");
        }
    }
}