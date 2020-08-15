using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Areas.GameSourceUser.ViewModels.UserProfileCommentViewModel;
using GameSource.Services.GameSourceUser.Contracts;
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

        public UserProfileCommentController(IUserProfileCommentService userProfileCommentService, IUserProfileService userProfileService, IUserService userService)
        {
            this.userProfileCommentService = userProfileCommentService;
            this.userProfileService = userProfileService;
            this.userService = userService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index(UserProfileCommentIndexViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserProfileCommentCreateViewModel viewModel)
        {
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