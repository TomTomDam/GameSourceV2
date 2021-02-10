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
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            this.userRoleService = userRoleService;
        }

        [HttpGet("GetAll")]
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
        public async Task Insert([FromBody] UserRole userRole)
        {
            await userRoleService.InsertAsync(userRole);
        }

        [HttpPost]
        public async Task Update([FromBody] UserRole userRole)
        {
            await userRoleService.UpdateAsync(userRole);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await userRoleService.DeleteAsync(id);
        }
    }
}