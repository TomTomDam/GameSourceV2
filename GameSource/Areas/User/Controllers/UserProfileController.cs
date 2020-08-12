using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using GameSource.ViewModels.GameSourceUser.UserViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Areas.User.Controllers
{
    [Area("User")]
    [Route("user/profile")]
    public class UserProfileController : Controller
    {
        private readonly IUserService userService;

        public UserProfileController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Profile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GameSource.Models.GameSourceUser.User user = await userService.GetByIDAsync((int)id);
            if (user == null)
            {
                return NotFound();
            }

            UserProfileViewModel viewModel = new UserProfileViewModel
            {

            };

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create(ViewResult viewModel)
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(ViewResult viewModel)
        {
            return View();
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id)
        {
            return View();
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(ViewResult viewModel)
        {
            return View();
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            return View();
        }
    }
}