using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Areas.Admin.ViewModels.UserViewModel;
using GameSource.Models.GameSourceUser;
using GameSource.Models.GameSourceUser.Enums;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameSource.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("admin/users")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserRoleService userRoleService;
        private readonly IUserStatusService userStatusService;
        private readonly IUserProfileService userProfileService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<UserRole> roleManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserController(IUserService userService, IUserRoleService userRoleService, IUserStatusService userStatusService, IUserProfileService userProfileService, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userService = userService;
            this.userRoleService = userRoleService;
            this.userStatusService = userStatusService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.webHostEnvironment = webHostEnvironment;
            this.userProfileService = userProfileService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            AdminUserIndexViewModel viewModel = new AdminUserIndexViewModel
            {
                Users = await userService.GetAllAsync(),
                UserRoles = await userRoleService.GetAllAsync(),
                UserStatuses = await userStatusService.GetAllAsync()
            };

            return View(viewModel);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int? id)
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

            return View(viewModel);
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            AdminUserRegisterViewModel viewModel = new AdminUserRegisterViewModel();
            return View(viewModel);
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AdminUserRegisterViewModel viewModel)
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

                if (user.UserProfile == null)
                {
                    //Create a new UserProfile after creating a new User - do not assign User to UserProfile until User is successfully created
                    UserProfile userProfile = new UserProfile
                    {
                        DisplayName = null,
                        Biography = null,
                        UserProfileVisibilityID = (int)UserProfileVisibilityEnum.Everyone,
                        UserProfileCommentPermissionID = (int)UserProfileCommentPermissionEnum.Everyone
                    };

                    //Add a default UserProfile Avatar Image
                    string fileName = Path.GetFileName("default_avatar.png");
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images\\UserProfile\\Avatar", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await userProfile.AvatarImage.CopyToAsync(fileStream);
                    }

                    user.UserProfile = userProfile;
                }

                var result = await userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    //Assign new UserProfile after user is created
                    user.UserProfile.UserID = user.Id;
                    await userProfileService.InsertAsync(user.UserProfile);

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

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
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
                UserRoleID = user.UserRoleID,
                UserRoles = userRolesSelectList,
                UserStatusID = user.UserStatusID,
                UserStatuses = userStatusesSelectList
            };

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminUserEditViewModel viewModel)
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
            user.UserStatusID = viewModel.UserStatusID;

            UserRole newUserRole = await userRoleService.GetByIDAsync(viewModel.UserRoleID);
            if (newUserRole == null)
            {
                return NotFound();
            }

            var newRoleResult = await userManager.AddToRoleAsync(user, newUserRole.Name);
            if (newRoleResult.Succeeded)
            {
                UserRole oldUserRole = await userRoleService.GetByIDAsync(user.UserRoleID);
                if (oldUserRole == null)
                {
                    return NotFound();
                }

                var removeRoleResult = await userManager.RemoveFromRoleAsync(user, oldUserRole.Name);
                if (removeRoleResult.Succeeded)
                {
                    user.UserRoleID = viewModel.UserRoleID;

                    await userService.UpdateAsync(user);
                    return RedirectToAction("Index", user);
                }

                foreach (var error in removeRoleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            foreach (var error in newRoleResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
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

            return View(user);
        }

        [HttpPost("delete/{id}"), ActionName("delete/{id}")]
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
                UserRole userRole = await userRoleService.GetByIDAsync(user.UserRoleID);
                user.UserRole = userRole;

                var userRoleResult = await userManager.RemoveFromRoleAsync(user, user.UserRole.Name);
                if (userRoleResult.Succeeded)
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

                foreach (var error in userRoleResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToAction("Index", userManager.Users);
        }

        [HttpGet("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(int? id)
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

        [HttpPost("deactivate/{id}"), ActionName("deactivate/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                user.UserStatusID = (int)UserStatusEnum.Deactivated;
            }

            return RedirectToAction("Index", userManager.Users);
        }

        [HttpGet("access-denied")]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Areas/Admin/Views/Shared/AccessDenied.cshtml");
        }
    }
}