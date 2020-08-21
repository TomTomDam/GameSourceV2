using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Areas.GameSourceUser.ViewModels.UserProfileCommentViewModel;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Areas.GameSourceUser.Controllers
{
    [Area("User")]
    [Route("user/profile/comment")]
    public class UserProfileCommentController : Controller
    {
        private readonly IUserProfileCommentService userProfileCommentService;
        private readonly IUserProfileService userProfileService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public UserProfileCommentController(IUserProfileCommentService userProfileCommentService, IUserProfileService userProfileService, IUserService userService, UserManager<User> userManager)
        {
            this.userProfileCommentService = userProfileCommentService;
            this.userProfileService = userProfileService;
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index(UserProfileCommentIndexViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            UserProfileCommentCreateViewModel viewModel = new UserProfileCommentCreateViewModel
            {
                UserProfileComment = new UserProfileComment()
            };

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserProfileCommentCreateViewModel viewModel)
        {
            UserProfileComment userProfileComment = new UserProfileComment
            {
                ID = viewModel.UserProfileComment.ID,
                Body = viewModel.UserProfileComment.Body,
                DateCreated = viewModel.UserProfileComment.DateCreated,
                CreatedByID = userManager.GetUserAsync(HttpContext.User).Result.Id,
                CreatedBy = await userManager.GetUserAsync(HttpContext.User),
                UserProfileID = viewModel.UserProfileComment.UserProfileID,
                UserProfile = viewModel.UserProfileComment.UserProfile
            };

            await userProfileCommentService.InsertAsync(userProfileComment);
            return View(viewModel);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            return View();
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(UserProfileCommentDeleteViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}