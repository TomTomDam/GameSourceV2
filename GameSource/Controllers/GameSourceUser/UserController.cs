using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Identity;
using GameSource.ViewModels.GameSourceUser.UserViewModel;
using GameSource.Models.GameSourceUser.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace GameSource.Controllers.GameSourceUser
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserRoleService userRoleService;
        private readonly IUserStatusService userStatusService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserController(IUserService userService, IUserRoleService userRoleService, IUserStatusService userStatusService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userService = userService;
            this.userRoleService = userRoleService;
            this.userStatusService = userStatusService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UserIndexViewModel viewModel = new UserIndexViewModel
            {
                Users = await userService.GetAllAsync(),
                UserRoles = await userRoleService.GetAllAsync(),
                UserStatuses = await userStatusService.GetAllAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
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

            UserDetailsViewModel viewModel = new UserDetailsViewModel
            {
                User = user,
                UserRole = await userRoleService.GetByIDAsync(user.UserRoleID),
                UserStatus = await userStatusService.GetByIDAsync(user.UserStatusID)
            };

            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            UserRegisterViewModel viewModel = new UserRegisterViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = viewModel.Username,
                    Email = viewModel.Email,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    DateCreated = DateTime.Now,
                    UserStatusID = (int)UserStatusEnum.Active,
                    UserRoleID = (int)UserRoleEnum.Member
                };

                var result = await userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    User newUser = await userManager.FindByNameAsync(user.UserName);
                    var roleResult = await userManager.AddToRoleAsync(newUser, "Member");

                    if (roleResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            IEnumerable<UserRole> userRoles = await userRoleService.GetAllAsync();
            List<SelectListItem> userRolesSelectList = userRoles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            IEnumerable<UserStatus> userStatuses = await userStatusService.GetAllAsync();
            List<SelectListItem> userStatusesSelectList = userStatuses.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            UserEditViewModel viewModel = new UserEditViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Age = user.Age,
                Location = user.Location,
                AvatarFilePath = user.AvatarFilePath,
                AvatarImage = user.AvatarImage,
                Description = user.Description,
                UserRoles = userRolesSelectList,
                UserStatuses = userStatusesSelectList
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel viewModel)
        {
            User user = await userManager.FindByIdAsync(viewModel.ID.ToString());
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = viewModel.FirstName;
            user.LastName = viewModel.LastName;
            user.Email = viewModel.Email;
            user.Age = viewModel.Age;
            user.Location = viewModel.Location;
            user.AvatarFilePath = viewModel.AvatarFilePath;
            user.AvatarImage = viewModel.AvatarImage;
            user.Description = viewModel.Description;
            user.UserStatusID = viewModel.UserStatusID;
            user.UserRoleID = viewModel.UserRoleID;

            userService.Update(user);
            return RedirectToAction("Index", user);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToAction("Index", userManager.Users);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }
    }
}
