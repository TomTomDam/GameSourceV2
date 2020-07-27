using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using GameSource.ViewModels.GameSourceUser.UserViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers.GameSourceUser
{
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> Profile(int? id)
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

            UserProfileViewModel viewModel = new UserProfileViewModel
            {

            };

            return View(viewModel);
        }
    }
}