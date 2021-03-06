using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace GameSource.API.Areas.Admin
{
    [Route("api/admin/users")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Gets all Users
        /// </summary>
        /// <response code="200">Returns a list of Users</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<User> result = await userRepository.GetAllAsync();

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User list.", result.Count());
        }

        /// <summary>
        /// Gets a User by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a User</response>
        /// <response code="404">Could not find a User</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(Guid id)
        {
            if (id == Guid.Empty)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var result = await userRepository.GetByIDAsync(id);
            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.NotFound, "User was not found.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User.", 1);
        }

        /// <summary>
        /// Creates a new User
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "firstName": "Tom",
        ///         "lastName": "Dam",
        ///         "age": 23,
        ///         "location": "Barnsley",
        ///         "userStatus": 1,
        ///         "userRole": 5f849c13-57bb-49e4-b841-0401b098d6b1
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new User</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] User user)
        {
            var inserted = await userRepository.InsertAsync(user);
            if (!inserted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not create a User.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully created a new User.", 1);
        }

        /// <summary>
        /// Updates a User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "firstName": "Tom",
        ///         "lastName": "Dam",
        ///         "age": 23,
        ///         "location": "Barnsley",
        ///         "userName": "TomDam",
        ///         "userStatus": 1,
        ///         "userRole": 1
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a User</response>
        /// <response code="404">Could not find a User</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(Guid id, [FromBody] User user)
        {
            if (id == Guid.Empty)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var updatedUser = await userRepository.GetByIDAsync(id);
            if (updatedUser == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "User was not found.");

            updatedUser.FirstName = user.FirstName;
            updatedUser.LastName = user.LastName;
            updatedUser.Age = user.Age;
            updatedUser.Location = updatedUser.Location;
            updatedUser.UserName = updatedUser.UserName;
            updatedUser.UserStatusID = user.UserStatusID;
            updatedUser.UserRoleID = user.UserRoleID;

            var updated = await userRepository.UpdateAsync(updatedUser);
            if (!updated)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update User.", 0);

            return new ApiResponse(updatedUser, ResponseStatusCode.Success, "Successfully updated User.", 1);
        }

        /// <summary>
        /// Deletes a User
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a User</response>
        /// <response code="404">Could not find a User</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(Guid id)
        {
            if (id == Guid.Empty)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            User user = await userRepository.GetByIDAsync(id);
            if (user == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "User was not found.");

            var deleted = await userRepository.DeleteAsync(user);
            if (!deleted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete User.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted User.", 1);
        }
    }
}