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
    [EnableCors("AllowOrigin")]
    public class UserStatusController : ControllerBase
    {
        private readonly IUserStatusRepository userStatusRepository;

        public UserStatusController(IUserStatusRepository userStatusRepository)
        {
            this.userStatusRepository = userStatusRepository;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserStatus> result = await userStatusRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User Status list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User Status list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userStatusRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User Status.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User Status.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserStatus userStatus)
        {
            int rows = await userStatusRepository.InsertAsync(userStatus);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a User Status.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new User Status.");
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserStatus userStatus)
        {
            int rows = await userStatusRepository.UpdateAsync(userStatus);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update User Status.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated User Status.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await userStatusRepository.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete User Status.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted User Status.");
        }
    }
}