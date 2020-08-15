using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Areas.GameSourceUser.ViewModels.UserProfileCommentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Areas.GameSourceUser.Controllers
{
    [Area("User")]
    [Route("user/profile/comment")]
    public class UserProfileCommentController : Controller
    {
        public UserProfileCommentController()
        {

        }

        [HttpGet("index")]
        public IActionResult Index(UserProfileCommentIndexViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserProfileCommentCreateViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            return View();
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(UserProfileCommentDeleteViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}