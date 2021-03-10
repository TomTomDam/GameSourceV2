using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Areas.GameSourceUser
{
    [Route("api/users")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<User> result = await userService.GetAllAsync();

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User list.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userService.GetByIDAsync(id);

            if (result != null)
                return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User.");

            return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] User user)
        {
            int rows = await userService.InsertAsync(user);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new User.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a User.");
        }

        [HttpPut]
        public async Task<ApiResponse> Update([FromBody] User user)
        {
            int rows = await userService.UpdateAsync(user);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated User.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update User.");
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            int rows = await userService.DeleteAsync(id);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted User.");

            return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete User.");
        }
    }
}