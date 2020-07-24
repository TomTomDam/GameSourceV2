using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Areas.Admin.ViewModels.UserRoleViewModel;
using GameSource.Areas.Admin.ViewModels.UserStatusViewModel;
using GameSource.Areas.Admin.ViewModels.UserViewModel;
using GameSource.Models.GameSourceUser;
using GameSource.Models.GameSourceUser.Enums;
using GameSource.Services.GameSourceUser.Contracts;
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
            AdminUserIndexViewModel viewModel = new AdminUserIndexViewModel
            {
                Users = await userService.GetAllAsync(),
                UserRoles = await userRoleService.GetAllAsync(),
                UserStatuses = await userStatusService.GetAllAsync()
            };

            return View("~/Areas/Admin/Views/User/Index.cshtml", viewModel);
        }

        [HttpGet("User/Details/{id}")]
        public async Task<IActionResult> UserDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userService.GetByIDAsync((int)id);
            if (user == null)
            {
                return NotFound();
            }

            AdminUserDetailsViewModel viewModel = new AdminUserDetailsViewModel
            {
                User = user,
                UserRole = await userRoleService.GetByIDAsync(user.UserRoleID),
                UserStatus = await userStatusService.GetByIDAsync(user.UserStatusID)
            };

            return View("~/Areas/Admin/Views/User/Details.cshtml", viewModel);
        }

        [HttpGet("User/Register")]
        public IActionResult RegisterUser()
        {
            AdminUserRegisterViewModel viewModel = new AdminUserRegisterViewModel();
            return View("~/Areas/Admin/Views/User/Register.cshtml", viewModel);
        }

        [HttpPost("User/Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(AdminUserRegisterViewModel viewModel)
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
        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

            AdminUserEditViewModel viewModel = new AdminUserEditViewModel
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
        public async Task<IActionResult> EditUser(AdminUserEditViewModel viewModel)
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
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            return View("~/Areas/Admin/Views/User/Delete.cshtml", user);
        }

        [HttpPost("User/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
            AdminUserRoleIndexViewModel viewModel = new AdminUserRoleIndexViewModel
            {
                UserRoles = await userRoleService.GetAllAsync()
            };

            return View("~/Areas/Admin/Views/UserRole/Index.cshtml", viewModel);
        }

        [HttpGet("UserRole/Details/{id}")]
        public async Task<IActionResult> UserRoleDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await userRoleService.GetByIDAsync((int)id);
            if (userRole == null)
            {
                return NotFound();
            }

            AdminUserRoleDetailsViewModel viewModel = new AdminUserRoleDetailsViewModel
            {
                UserRole = userRole
            };

            return View("~/Areas/Admin/Views/UserRole/Details.cshtml", viewModel);
        }

        [HttpGet("UserRole/Create")]
        public IActionResult CreateUserRole()
        {
            AdminUserRoleCreateViewModel viewModel = new AdminUserRoleCreateViewModel();
            return View("~/Areas/Admin/Views/UserRole/Create.cshtml", viewModel);
        }

        [HttpPost("UserRole/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUserRole(AdminUserRoleCreateViewModel viewModel)
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
        public async Task<IActionResult> EditUserRole(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await roleManager.FindByIdAsync(id.ToString());
            if (userRole == null)
            {
                return NotFound();
            }

            AdminUserRoleEditViewModel viewModel = new AdminUserRoleEditViewModel
            {
                ID = userRole.Id,
                Name = userRole.Name,
                Description = userRole.Description
            };

            return View("~/Areas/Admin/Views/UserRole/Edit.cshtml", viewModel);
        }

        [HttpPost("UserRole/Edit")]
        public async Task<IActionResult> EditUserRole(AdminUserRoleEditViewModel viewModel)
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
        public async Task<IActionResult> DeleteUserRole(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await roleManager.FindByIdAsync(id.ToString());
            if (userRole == null)
            {
                return NotFound();
            }

            return View("~/Areas/Admin/Views/UserRole/Delete.cshtml", userRole);
        }

        [HttpPost("UserRole/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserRoleConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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

        #region UserStatus
        [HttpGet("UserStatus/Index")]
        public async Task<IActionResult> UserStatusIndex()
        {
            AdminUserStatusIndexViewModel viewModel = new AdminUserStatusIndexViewModel
            {
                UserStatuses = await userStatusService.GetAllAsync()
            };

            return View("~/Areas/Admin/Views/UserStatus/Index.cshtml", viewModel);
        }

        [HttpGet("UserStatus/Details/{id}")]
        public async Task<IActionResult> UserStatusDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AdminUserStatusDetailsViewModel viewModel = new AdminUserStatusDetailsViewModel();
            viewModel.UserStatus = await userStatusService.GetByIDAsync((int)id);

            return View("~/Areas/Admin/Views/UserStatus/Details.cshtml", viewModel);
        }

        [HttpGet("UserStatus/Create")]
        public async Task<IActionResult> CreateUserStatus()
        {
            AdminUserStatusCreateViewModel viewModel = new AdminUserStatusCreateViewModel();
            return View("~/Areas/Admin/Views/UserStatus/Create.cshtml", viewModel);
        }

        [HttpPost("UserStatus/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUserStatus(AdminUserStatusCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserStatus userStatus = new UserStatus
                {
                    Name = viewModel.Name
                };

                userStatusService.Insert(userStatus);
                return RedirectToAction("UserStatusIndex");
            }

            return View("~/Areas/Admin/Views/UserStatus/Create.cshtml", viewModel);
        }

        [HttpGet("UserStatus/Edit/{id}")]
        public async Task<IActionResult> EditUserStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserStatus userStatus = await userStatusService.GetByIDAsync((int)id);
            if (userStatus == null)
            {
                return NotFound();
            }

            AdminUserStatusEditViewModel viewModel = new AdminUserStatusEditViewModel
            {
                ID = userStatus.Id,
                Name = userStatus.Name
            };

            return View("~/Areas/Admin/Views/UserStatus/Edit.cshtml", viewModel);
        }

        [HttpPost("UserStatus/Edit")]
        public async Task<IActionResult> EditUserStatus(AdminUserStatusEditViewModel viewModel)
        {
            UserStatus userStatus = await userStatusService.GetByIDAsync(viewModel.ID);
            if (userStatus == null)
            {
                return NotFound();
            }

            userStatus.Id = viewModel.ID;
            userStatus.Name = viewModel.Name;

            userStatusService.Update(userStatus);
            return RedirectToAction("UserStatusIndex");
        }

        [HttpGet("UserStatus/Delete/{id}")]
        public async Task<IActionResult> DeleteUserStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserStatus userStatus = await userStatusService.GetByIDAsync((int)id);
            if (userStatus == null)
            {
                return NotFound();
            }

            AdminUserStatusDeleteViewModel viewModel = new AdminUserStatusDeleteViewModel
            {
                ID = userStatus.Id,
                Name = userStatus.Name
            };

            return View("~/Areas/Admin/Views/UserStatus/Delete.cshtml", viewModel);
        }

        [HttpPost("UserStatus/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserStatus(AdminUserStatusDeleteViewModel viewModel)
        {
            UserStatus userStatus = await userStatusService.GetByIDAsync(viewModel.ID);
            if (userStatus != null)
            {
                await userStatusService.DeleteAsync(userStatus.Id);
                return RedirectToAction("UserStatusIndex");
            }

            return View("~/Areas/Admin/Views/UserStatus/Delete.cshtml");
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