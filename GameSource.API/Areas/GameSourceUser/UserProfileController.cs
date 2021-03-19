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
    [Route("api/user-profiles")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AllowOrigin")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileRepository userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        /// <summary>
        /// Gets all UserProfiles
        /// </summary>
        /// <response code="200">Returns a list of UserProfiles</response>
        /// <response code="400">Request failed</response>
        [HttpGet]
        public async Task<ApiResponse> GetAll()
        {
            IEnumerable<UserProfile> result = await userProfileRepository.GetAllAsync();

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return UserProfile list.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned UserProfile list.");
        }

        /// <summary>
        /// Gets a UserProfile by its ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a UserProfile</response>
        /// <response code="404">Could not find a UserProfile</response>
        /// <response code="400">Request failed</response>
        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByID(int id)
        {
            var result = await userProfileRepository.GetByIDAsync(id);

            if (result == null)
                return new ApiResponse(result, ResponseStatusCode.Error, "Could not return a UserProfile.");

            return new ApiResponse(result, ResponseStatusCode.Success, "Successfully returned a UserProfile.", 1);
        }

        /// <summary>
        /// Updates a UserProfile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userProfile"></param>
        /// <remarks>
        /// Example request:
        /// 
        ///     {
        ///         "displayName": "TomDam",
        ///         "biography": "This is my profile!",
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">Updated a UserProfile</response>
        /// <response code="404">Could not find a UserProfile</response>
        /// <response code="400">Request failed</response>
        [HttpPut("{id}")]
        public async Task<ApiResponse> Update(int id, [FromBody] UserProfile userProfile)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            var updatedUserProfile = await userProfileRepository.GetByIDAsync(id);
            if (updatedUserProfile == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "UserProfile was not found.");

            updatedUserProfile.Biography = userProfile.Biography;
            updatedUserProfile.DisplayName = userProfile.DisplayName;
            updatedUserProfile.ProfileBackgroundImageFilePath = userProfile.ProfileBackgroundImageFilePath;
            updatedUserProfile.UserProfileCommentPermissionID = userProfile.UserProfileCommentPermissionID;
            updatedUserProfile.UserProfileVisibilityID = updatedUserProfile.UserProfileVisibilityID;

            var updated = await userProfileRepository.UpdateAsync(updatedUserProfile);
            if (!updated)
                return new ApiResponse(ResponseStatusCode.Error, "Could not update UserProfile.", 0);

            return new ApiResponse(updatedUserProfile, ResponseStatusCode.Success, "Successfully updated UserProfile.", 1);
        }

        /// <summary>
        /// Deletes a UserProfile
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Deleted a UserProfile</response>
        /// <response code="404">Could not find a UserProfile</response>
        /// <response code="400">Request failed</response>
        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            if (id == 0)
                return new ApiResponse(ResponseStatusCode.Error, "Invalid ID. Please check the ID.");

            UserProfile userProfile = await userProfileRepository.GetByIDAsync(id);
            if (userProfile == null)
                return new ApiResponse(ResponseStatusCode.NotFound, "UserProfile was not found.");

            var deleted = await userProfileRepository.DeleteAsync(userProfile);
            if (!deleted)
                return new ApiResponse(ResponseStatusCode.Error, "Could not delete UserProfile.", 0);

            return new ApiResponse(ResponseStatusCode.Success, "Successfully deleted UserProfile.", 1);
        }
    }
}