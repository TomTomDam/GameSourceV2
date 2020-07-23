using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Areas.Admin.ViewModels;
using GameSource.Models.GameSourceUser;
using GameSource.Models.GameSourceUser.Enums;
using GameSource.Services.GameSourceUser.Contracts;
using GameSource.ViewModels.GameSourceUser.UserRoleViewModel;
using GameSource.ViewModels.GameSourceUser.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameSource.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserRoleService userRoleService;
        private readonly IUserStatusService userStatusService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<UserRole> roleManager;

        public AdminController(IUserService userService, IUserRoleService userRoleService, IUserStatusService userStatusService, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager)
        {
            this.userService = userService;
            this.userRoleService = userRoleService;
            this.userStatusService = userStatusService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        #region User
        [HttpGet("User/Index")]
        public async Task<IActionResult> UserIndex()
        {
            UserIndexViewModel viewModel = new UserIndexViewModel
            {
                Users = await userService.GetAllAsync(),
                UserRoles = await userRoleService.GetAllAsync(),
                UserStatuses = await userStatusService.GetAllAsync()
            };

            return View("~/Areas/Admin/Views/User/Index.cshtml", viewModel);
        }

        [HttpGet("User/Details/{id}")]
        public async Task<IActionResult> UserDetails(int id)
        {
            var user = await userService.GetByIDAsync(id);
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

            return View("~/Areas/Admin/Views/User/Details.cshtml", viewModel);
        }

        [HttpGet("User/Register")]
        [AllowAnonymous]
        public IActionResult RegisterUser()
        {
            AdminRegisterUserViewModel viewModel = new AdminRegisterUserViewModel();
            return View("~/Areas/Admin/Views/User/Register.cshtml", viewModel);
        }

        [HttpPost("User/Register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(AdminRegisterUserViewModel viewModel)
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
                    var newUser = await userManager.FindByNameAsync(user.UserName);
                    var roleResult = await userManager.AddToRoleAsync(newUser, "Member");

                    if (roleResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("UserIndex");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(viewModel);
        }

        [HttpGet("User/Edit/{id}")]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await userRoleService.GetAllAsync();
            var userRolesSelectList = userRoles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            var userStatuses = await userStatusService.GetAllAsync();
            var userStatusesSelectList = userStatuses.Select(x => new SelectListItem()
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

            return View("~/Areas/Admin/Views/User/Edit.cshtml", viewModel);
        }

        [HttpPost("User/Edit")]
        public async Task<IActionResult> EditUser(UserEditViewModel viewModel)
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
            return RedirectToAction("UserIndex", user);
        }

        [HttpGet("User/Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            return View("~/Areas/Admin/Views/User/Delete.cshtml", user);
        }

        [HttpPost("User/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserIndex");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToAction("UserIndex", userManager.Users);
        }

        #endregion

        #region UserRole
        [HttpGet("UserRole/Index")]
        public async Task<IActionResult> UserRoleIndex()
        {
            UserRoleIndexViewModel viewModel = new UserRoleIndexViewModel
            {
                UserRoles = await userRoleService.GetAllAsync()
            };

            return View("~/Areas/Admin/Views/UserRole/Index.cshtml", viewModel);
        }

        [HttpGet("UserRole/Details/{id}")]
        public async Task<IActionResult> UserRoleDetails(int id)
        {
            var userRole = await userRoleService.GetByIDAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }

            UserRoleDetailsViewModel viewModel = new UserRoleDetailsViewModel
            {
                UserRole = userRole
            };

            return View("~/Areas/Admin/Views/UserRole/Details.cshtml", viewModel);
        }

        [HttpGet("UserRole/Create")]
        public IActionResult CreateUserRole()
        {
            UserRoleCreateViewModel viewModel = new UserRoleCreateViewModel();
            return View("~/Areas/Admin/Views/UserRole/Create.cshtml", viewModel);
        }

        [HttpPost("UserRole/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUserRole(UserRoleCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserRole userRole = new UserRole
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description
                };

                var result = await roleManager.CreateAsync(userRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserRoleIndex");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("~/Areas/Admin/Views/UserRole/Create.cshtml", viewModel);
        }

        [HttpGet("UserRole/Edit/{id}")]
        public async Task<IActionResult> EditUserRole(int id)
        {
            var userRole = await roleManager.FindByIdAsync(id.ToString());
            if (userRole == null)
            {
                return NotFound();
            }

            UserRoleEditViewModel viewModel = new UserRoleEditViewModel
            {
                ID = userRole.Id,
                Name = userRole.Name,
                Description = userRole.Description
            };

            return View("~/Areas/Admin/Views/UserRole/Edit.cshtml", viewModel);
        }

        [HttpPost("UserRole/Edit")]
        public async Task<IActionResult> EditUserRole(UserRoleEditViewModel viewModel)
        {
            UserRole userRole = await roleManager.FindByIdAsync(viewModel.ID.ToString());
            if (userRole == null)
            {
                return NotFound();
            }

            userRole.Id = viewModel.ID;
            userRole.Name = viewModel.Name;
            userRole.Description = viewModel.Description;

            userRoleService.Update(userRole);
            return RedirectToAction("UserRoleIndex");
        }

        [HttpGet("UserRole/Delete/{id}")]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            var userRole = await roleManager.FindByIdAsync(id.ToString());
            if (userRole == null)
            {
                return NotFound();
            }

            return View("~/Areas/Admin/Views/UserRole/Delete.cshtml", userRole);
        }

        [HttpPost("UserRole/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserRoleConfirmed(int id)
        {
            var userRole = await roleManager.FindByIdAsync(id.ToString());
            if (userRole != null)
            {
                var result = await roleManager.DeleteAsync(userRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserRoleIndex");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToAction("UserRoleIndex", roleManager.Roles);
        }
        #endregion

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Areas/Admin/Views/Shared/AccessDenied.cshtml");
        }
    }
}