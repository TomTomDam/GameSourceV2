using System;
using System.Collections.Generic;
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
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserProfileController(IUserService userService, IUserProfileService userProfileService, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userService = userService;
            this.userProfileService = userProfileService;
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

            UserProfileDetailsViewModel viewModel = new UserProfileDetailsViewModel
            {
                UserProfile = user.UserProfile,
                User = user,
                UserProfileComments = user.UserProfileCommentsCreated.ToList()
            };

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfileEditViewModel viewModel)
        {
            UserProfile userProfile = await userProfileService.GetByIDAsync(viewModel.UserProfile.ID);

            userProfile.DisplayName = viewModel.UserProfile.DisplayName;
            userProfile.Biography = viewModel.UserProfile.Biography;
            userProfile.UserProfileVisibility = viewModel.UserProfile.UserProfileVisibility;
            userProfile.UserProfileCommentPermission = viewModel.UserProfile.UserProfileCommentPermission;

            string fileName = Path.GetFileName(viewModel.UserProfile.AvatarImage.FileName);
            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images\\UserProfile\\Avatar", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await viewModel.UserProfile.AvatarImage.CopyToAsync(fileStream);
            }

            userProfile.AvatarImage = viewModel.UserProfile.AvatarImage;

            return RedirectToAction("Profile", userProfile);
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
        #endregion
    }
}