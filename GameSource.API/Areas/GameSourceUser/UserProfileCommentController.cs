using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSourceUser;
using GameSource.Infrastructure.Repositories.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Areas.GameSourceUser
{
    [Route("api/user-profile-comments")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class UserProfileCommentController : ControllerBase
    {
        private readonly IUserProfileCommentRepository userProfileCommentRepository;

        public UserProfileCommentController(IUserProfileCommentRepository userProfileCommentRepository)
        {
            this.userProfileCommentRepository = userProfileCommentRepository;
        }

        /// <summary>
        /// Gets all UserProfileComments
        /// </summary>
        /// <response code="200">Returns a list of UserProfileComments</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserProfileComment> result = await userProfileCommentRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return UserProfileComment list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned UserProfileComment list.");
        }

        /// <summary>
        /// Gets a UserProfileComment by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a UserProfileComment</response>
        /// <response code="404">Could not find a UserProfileComment</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userProfileCommentRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a UserProfileComment.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a UserProfileComment.");
        }

        /// <summary>
        /// Creates a new UserProfileComment
        /// </summary>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Creates a new UserProfileComment</response>
        /// <response code="400">Request failed</response>
        [HttpPost]
        public async Task<ApiResponse> Insert([FromBody] UserProfileComment userStatus)
        {
            int rows = await userProfileCommentRepository.InsertAsync(userStatus);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not create a UserProfileComment.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully created a new UserProfileComment.");
        }

        /// <summary>
        /// Updates a UserProfileComment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userProfileComment"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "name": "BioWare"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a UserProfileComment</response>
        /// <response code="404">Could not find a UserProfileComment</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserProfileComment userProfileComment)
        {
            int rows = await userProfileCommentRepository.UpdateAsync(userProfileComment);

            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not update UserProfileComment.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully updated UserProfileComment.");
        }

        /// <summary>
        /// Deletes a UserProfileComment
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a UserProfileComment</response>
        /// <response code="404">Could not find a UserProfileComment</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            UserProfileComment userProfileComment = await userProfileCommentRepository.GetByIDAsync(id);
            if (id == 0 || userProfileComment == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "UserProfileComment was not found. Please check the ID.");

            int rows = await userProfileCommentRepository.DeleteAsync(userProfileComment);
            if (rows <= 0)
                return new ApiResponse(rows, ResponseStatusCode.Error, "Could not delete UserProfileComment.");

            return new ApiResponse(rows, ResponseStatusCode.Success, "Successfully deleted UserProfileComment.");
        }
    }
}