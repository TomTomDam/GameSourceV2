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
    public class UserStatusController : ControllerBase
    {
        private readonly IUserStatusService userStatusService;

        public UserStatusController(IUserStatusService userStatusService)
        {
            this.userStatusService = userStatusService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserStatus> result = await userStatusService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned UserStatus list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return UserStatus list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userStatusService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a UserStatus.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a UserStatus.");
        }

        [HttpPost]
        public async Task Insert([FromBody] UserStatus userStatus)
        {
            await userStatusService.InsertAsync(userStatus);
        }

        [HttpPost]
        public async Task Update([FromBody] UserStatus userStatus)
        {
            await userStatusService.UpdateAsync(userStatus);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await userStatusService.DeleteAsync(id);
        }
    }
}