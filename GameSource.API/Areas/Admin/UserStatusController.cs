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
    [Route("api/user-statuses")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserStatusController : ControllerBase
    {
        private readonly IUserStatusService userStatusService;

        public UserStatusController(IUserStatusService userStatusService)
        {
            this.userStatusService = userStatusService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserStatus> result = await userStatusService.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User Status list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User Status list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userStatusService.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User Status.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User Status.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserStatus userStatus)
        {
            int rows = await userStatusService.InsertAsync(userStatus);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a User Status.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new User Status.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserStatus userStatus)
        {
            int rows = await userStatusService.UpdateAsync(userStatus);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update User Status.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated User Status.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await userStatusService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete User Status.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted User Status.");
        }
    }
}