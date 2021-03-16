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

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return User list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned User list.");
        }

        /// <summary>
        /// Gets a User by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a User</response>
        /// <response code="404">Could not find a User</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a User.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a User.");
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
        ///         "userRole": 1
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new User</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] User user)
        {
            int rows = await userRepository.InsertAsync(user);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a User.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new User.");
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
        ///         "userStatus": 1,
        ///         "userRole": 1
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a User</response>
        /// <response code="404">Could not find a User</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] User user)
        {
            int rows = await userRepository.UpdateAsync(user);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update User.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated User.");
        }

        /// <summary>
        /// Deletes a User
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a User</response>
        /// <response code="404">Could not find a User</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            User user = await userRepository.GetByIDAsync(id);
            if (id == 0 || user == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "User was not found. Please check the ID.");

            int rows = await userRepository.DeleteAsync(user);
            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete User.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted User.");
        }
    }
}