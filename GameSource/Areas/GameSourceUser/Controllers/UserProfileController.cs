using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Areas.GameSourceUser.ViewModels.UserProfileViewModel;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Areas.GameSourceUser.Controllers
{
    [Area("GameSourceUser")]
    [Route("user/profile")]
    public class UserProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserProfileService userProfileService;
        private readonly IUserProfileVisibilityService userProfileVisibilityService;
        private readonly IUserProfileCommentPermissionService userProfileCommentPermissionService;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserProfileController(IUserService userService, IUserProfileService userProfileService, IUserProfileVisibilityService userProfileVisibilityService, IUserProfileCommentPermissionService userProfileCommentPermissionService, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userService = userService;
            this.userProfileService = userProfileService;
            this.userProfileVisibilityService = userProfileVisibilityService;
            this.userProfileCommentPermissionService = userProfileCommentPermissionService;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await userService.GetByIDAsync((int)id);
            if (user == null || user.UserProfile == null)
            {
                return NotFound();
            }

            UserProfileVisibility userProfileVisibility = await userProfileVisibilityService.GetByUserProfileIDAsync(user.UserProfile.ID);
            if (userProfileVisibility == null)
            {
                return NotFound();
            }

            UserProfileCommentPermission userProfileCommentPermission = await userProfileCommentPermissionService.GetByIDAsync((int)user.UserProfile.UserProfileCommentPermissionID);
            if (userProfileCommentPermission == null)
            {
                return NotFound();
            }

            UserProfileDetailsViewModel viewModel = new UserProfileDetailsViewModel
            {
                UserProfile = user.UserProfile,
                User = user,
                UserProfileComments = user.UserProfileCommentsCreated.ToList(),
                UserProfileVisibility = userProfileVisibility,
                UserProfileCommentPermission = userProfileCommentPermission
            };

            return View(viewModel);
        }

        #region Profile Settings
        [HttpGet("{id}/profile-settings")]
        public async Task<IActionResult> ProfileSettings(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile userProfile = await userProfileService.GetByUserIDAsync((int)id);
            if (userProfile == null)
            {
                return NotFound();
            }

            UserProfileEditViewModel viewModel = new UserProfileEditViewModel
            {
                UserProfile = userProfile
            };

            return View(viewModel);
        }

        [HttpGet("{id}/general-settings")]
        public async Task<IActionResult> GeneralSettings(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile userProfile = await userProfileService.GetByIDAsync((int)id);
            if (userProfile == null)
            {
                return NotFound();
            }

            UserProfileEditViewModel viewModel = new UserProfileEditViewModel
            {
                UserProfile = userProfile
            };

            return PartialView("_GeneralSettings", viewModel);
        }

        [HttpPost("{id}/general-settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GeneralSettings(UserProfileEditViewModel viewModel)
        {
            UserProfile userProfile = await userProfileService.GetByIDAsync(viewModel.UserProfile.ID);

            userProfile.Biography = viewModel.UserProfile.Biography;
            await userProfileService.UpdateAsync(userProfile);

            return RedirectToAction("Profile", new { id = userProfile.UserID });
        }

        [HttpGet("{id}/avatar-settings")]
        public async Task<IActionResult> AvatarSettings(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile userProfile = await userProfileService.GetByIDAsync((int)id);
            if (userProfile == null)
            {
                return NotFound();
            }

            UserProfileEditViewModel viewModel = new UserProfileEditViewModel
            {
                UserProfile = userProfile
            };

            return PartialView("_AvatarSettings", viewModel);
        }

        [HttpPost("{id}/avatar-settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AvatarSettings(UserProfileEditViewModel viewModel)
        {
            UserProfile userProfile = await userProfileService.GetByIDAsync(viewModel.UserProfile.ID);

            string uniqueFileName = null;
            string avatarImageFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\UserProfile\\Avatar");
            if (viewModel.UserProfile.AvatarImage != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.UserProfile.AvatarImage.FileName;
                string filePath = Path.Combine(avatarImageFolder, uniqueFileName);
                await viewModel.UserProfile.AvatarImage.CopyToAsync(new FileStream(filePath, FileMode.Create));

                userProfile.AvatarFilePath = uniqueFileName;
                userProfile.AvatarImage = viewModel.UserProfile.AvatarImage;
                await userProfileService.UpdateAsync(userProfile);
            }

            return RedirectToAction("Profile", new { id = userProfile.UserID });
        }

        [HttpGet("{id}/profile-background-settings")]
        public async Task<IActionResult> ProfileBackgroundSettings(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile userProfile = await userProfileService.GetByIDAsync((int)id);
            if (userProfile == null)
            {
                return NotFound();
            }

            UserProfileEditViewModel viewModel = new UserProfileEditViewModel
            {
                UserProfile = userProfile
            };

            return PartialView("_ProfileBackgroundSettings", viewModel);
        }

        [HttpPost("{id}/profile-background-settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfileBackgroundSettings(UserProfileEditViewModel viewModel)
        {
            UserProfile userProfile = await userProfileService.GetByIDAsync(viewModel.UserProfile.ID);

            //string uniqueFileName = null;
            //string profileBackgroundImageFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\UserProfile\\Background");
            //if (viewModel.UserProfile.AvatarImage != null)
            //{
            //    uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.UserProfile.ProfileBackgroundImage.FileName;
            //    string filePath = Path.Combine(avatarImageFolder, uniqueFileName);
            //    await viewModel.UserProfile.ProfileBackgroundImage.CopyToAsync(new FileStream(filePath, FileMode.Create));

            //    userProfile.ProfileBackgroundFilePath = uniqueFileName;
            //    userProfile.ProfileBackgroundImage = viewModel.UserProfile.ProfileBackgroundImage;
            //    await userProfileService.UpdateAsync(userProfile);
            //}

            return RedirectToAction("Profile", new { id = userProfile.UserID });
        }
        #endregion
    }
}