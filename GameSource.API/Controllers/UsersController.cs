using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            var result = await userService.GetAllAsync();

            if (result.Any())
            {
                return new ApiResponse(ResponseStatusCode.Success, "Successfully returned Users list.");
            }

            return new ApiResponse(ResponseStatusCode.Error, "Could not return Users list.");
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userService.GetByIDAsync(id);

            if (result != null)
            {
                return new ApiResponse(ResponseStatusCode.Success, "Successfully returned a User.");
            }

            return new ApiResponse(ResponseStatusCode.Error, "Could not return a User.");
        }

        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] User user)
        {
            var result = await userService.InsertAsync(user);

            if (result != null)
            {
                return new ApiResponse(ResponseStatusCode.Success, "Successfully created a User.");
            }

            return new ApiResponse(ResponseStatusCode.Error, "Could not create a User.");
        }

        [HttpPost]
        public async Task Update([FromBody] User user)
        {
            await userService.UpdateAsync(user);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await userService.DeleteAsync(id);
        }
    }
}
