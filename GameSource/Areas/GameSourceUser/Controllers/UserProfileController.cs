using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Areas.GameSourceUser.ViewModels.UserProfileViewModel;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserProfileController(IUserService userService, IUserProfileService userProfileService, IUserProfileVisibilityService userProfileVisibilityService, IUserProfileCommentPermissionService userProfileCommentPermissionService, IWebHostEnvironment webHostEnvironment)
        {
            this.userService = userService;
            this.userProfileService = userProfileService;
            this.userProfileVisibilityService = userProfileVisibilityService;
            this.userProfileCommentPermissionService = userProfileCommentPermissionService;
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
        public async Task<IActionResult> GeneralSettingsPartial(int? id)
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
        public async Task<IActionResult> GeneralSettingsPartial(UserProfileEditViewModel viewModel)
        {
            UserProfile userProfile = await userProfileService.GetByIDAsync(viewModel.UserProfile.ID);

            userProfile.Biography = viewModel.UserProfile.Biography;

            await userProfileService.UpdateAsync(userProfile);
            return RedirectToAction("Profile", new { id = userProfile.UserID });
        }

        [HttpGet("{id}/avatar-settings")]
        public async Task<IActionResult> AvatarSettingsPartial(int? id)
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
        public async Task<IActionResult> AvatarSettingsPartial(UserProfileEditViewModel viewModel)
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

        [HttpGet("{id}/privacy-settings")]
        public async Task<IActionResult> PrivacySettingsPartial(int? id)
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

            var visibilityList = await userProfileVisibilityService.GetAllAsync();
            var visibilitySelectList = visibilityList.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            var commentPermissionList = await userProfileCommentPermissionService.GetAllAsync();
            var commentPermissionSelectList = commentPermissionList.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            UserProfileEditViewModel viewModel = new UserProfileEditViewModel
            {
                UserProfile = userProfile,
                UserProfileVisibilityList = visibilitySelectList,
                UserProfileCommentPermissionList = commentPermissionSelectList
            };

            return PartialView("_PrivacySettings");
        }

        [HttpPost("{id}/privacy-settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PrivacySettingsPartial(UserProfileEditViewModel viewModel)
        {
            UserProfile userProfile = await userProfileService.GetByIDAsync(viewModel.UserProfile.ID);

            userProfile.UserProfileVisibility = viewModel.UserProfile.UserProfileVisibility;
            userProfile.UserProfileCommentPermission = viewModel.UserProfile.UserProfileCommentPermission;

            await userProfileService.UpdateAsync(userProfile);
            return RedirectToAction("Profile", new { id = userProfile.UserID });
        }

        [HttpGet("{id}/profile-background-settings")]
        public async Task<IActionResult> ProfileBackgroundSettingsPartial(int? id)
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
        public async Task<IActionResult> ProfileBackgroundSettingsPartial(UserProfileEditViewModel viewModel)
        {
            UserProfile userProfile = await userProfileService.GetByIDAsync(viewModel.UserProfile.ID);

            string uniqueFileName = null;
            string profileBackgroundImageFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\UserProfile\\Background");
            if (viewModel.UserProfile.AvatarImage != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.UserProfile.ProfileBackgroundImage.FileName;
                string filePath = Path.Combine(profileBackgroundImageFolder, uniqueFileName);
                await viewModel.UserProfile.ProfileBackgroundImage.CopyToAsync(new FileStream(filePath, FileMode.Create));

                userProfile.ProfileBackgroundImageFilePath = uniqueFileName;
                userProfile.ProfileBackgroundImage = viewModel.UserProfile.ProfileBackgroundImage;

                await userProfileService.UpdateAsync(userProfile);
            }

            return RedirectToAction("Profile", new { id = userProfile.UserID });
        }
        #endregion

        #region AccountSettings
        [HttpGet("{id}/account")]
        public async Task<IActionResult> AccountSettings(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await userService.GetByIDAsync((int)id);
            if (user == null)
            {
                return NotFound();
            }

            UserProfileEditViewModel viewModel = new UserProfileEditViewModel
            {
                User = user
            };

            return View(viewModel);
        }

        [HttpGet("{id}/account-settings")]
        public async Task<IActionResult> AccountSettingsPartial(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await userService.GetByIDAsync((int)id);
            if (user == null)
            {
                return NotFound();
            }

            UserProfileEditViewModel viewModel = new UserProfileEditViewModel
            {
                User = user
            };

            return PartialView("_AccountSettings", viewModel);
        }

        [HttpPost("{id}/account-settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountSettingsPartial(UserProfileEditViewModel viewModel)
        {
            User user = await userService.GetByIDAsync(viewModel.User.Id);

            user.FirstName = viewModel.User.FirstName;
            user.LastName = viewModel.User.LastName;
            user.Age = viewModel.User.Age;
            user.Location = viewModel.User.Location;

            await userService.UpdateAsync(user);
            return RedirectToAction("Profile", new { id = user.Id });
        }

        [HttpGet("{id}/email-settings")]
        public async Task<IActionResult> EmailSettingsPartial(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await userService.GetByIDAsync((int)id);
            if (user == null)
            {
                return NotFound();
            }

            UserProfileEditViewModel viewModel = new UserProfileEditViewModel
            {
                User = user,
                EmailAddress = user.Email
            };

            return PartialView("_EmailSettings", viewModel);
        }

        [HttpPost("{id}/email-settings")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmailSettingsPartial(UserProfileEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await userService.GetByIDAsync(viewModel.User.Id);

                user.Email = viewModel.EmailAddress;

                await userService.UpdateAsync(user);
                return RedirectToAction("Profile", new { id = user.Id });
            }

            return PartialView("_EmailSettings", viewModel);
        }
        #endregion
    }
}