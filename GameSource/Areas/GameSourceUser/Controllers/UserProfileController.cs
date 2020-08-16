using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Areas.GameSourceUser.ViewModels.UserProfileViewModel;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Areas.GameSourceUser.Controllers
{
    [Area("User")]
    [Route("user/profile")]
    public class UserProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserProfileService userProfileService;

        public UserProfileController(IUserService userService, IUserProfileService userProfileService)
        {
            this.userService = userService;
            this.userProfileService = userProfileService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Profile(int? id)
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


            User user = await userService.GetByIDAsync(userProfile.UserID);
            if (user == null)
            {
                return NotFound();
            }

            UserProfileDetailsViewModel viewModel = new UserProfileDetailsViewModel
            {
                UserProfile = userProfile,
                User = user
            };

            return View(viewModel);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile userProfile = await userProfileService.GetByIDAsync((int)id);

            UserProfileEditViewModel viewModel = new UserProfileEditViewModel
            {
                UserProfile = userProfile
            };

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfileEditViewModel viewModel)
        {
            UserProfile userProfile = await userProfileService.GetByIDAsync(viewModel.UserProfile.ID);

            userProfile.Biography = viewModel.UserProfile.Biography;
            userProfile.UserProfileVisibility = viewModel.UserProfile.UserProfileVisibility;
            userProfile.UserProfileCommentPermission = viewModel.UserProfile.UserProfileCommentPermission;

            return RedirectToAction("Profile", userProfile);
        }
    }
}