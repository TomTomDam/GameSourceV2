using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Identity;
using GameSource.ViewModels.GameSourceUser.AccountViewModel;
using GameSource.Models.GameSourceUser.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace GameSource.Controllers.GameSourceUser
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IUserRoleService userRoleService;
        private readonly IUserStatusService userStatusService;
        private readonly IUserProfileService userProfileService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AccountController(IUserService userService, IUserRoleService userRoleService, IUserStatusService userStatusService, IUserProfileService userProfileService, UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userService = userService;
            this.userRoleService = userRoleService;
            this.userStatusService = userStatusService;
            this.userProfileService = userProfileService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            AccountIndexViewModel viewModel = new AccountIndexViewModel
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

            User user = await userService.GetByIDAsync((int)id);
            if (user == null)
            {
                return NotFound();
            }

            AccountDetailsViewModel viewModel = new AccountDetailsViewModel
            {
                User = user,
                UserRole = await userRoleService.GetByIDAsync(user.UserRoleID),
                UserStatus = await userStatusService.GetByIDAsync(user.UserStatusID)
            };

            return View(viewModel);
        }

        [HttpGet("register")]
        [AllowAnonymous]
        public IActionResult Register()
        {
            AccountRegisterViewModel viewModel = new AccountRegisterViewModel();
            return View(viewModel);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountRegisterViewModel viewModel)
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
                    //Create a new UserProfile - do not assign User to UserProfile until User is successfully created
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
                    //Assign and create new UserProfile after user is created
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

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            AccountLoginViewModel viewModel = new AccountLoginViewModel();
            return View(viewModel);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, viewModel.RememberMe, false);

                if (result.Succeeded)
                {
                    User loggedInUser = await userManager.FindByNameAsync(viewModel.UserName);
                    if (loggedInUser.UserStatusID == (int)UserStatusEnum.Deactivated)
                    {
                        loggedInUser.UserStatusID = (int)UserStatusEnum.Active;
                        return RedirectToAction("Index", "Home");
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(viewModel);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #region Account Settings
        [HttpGet("account-settings")]
        public IActionResult AccountSettings()
        {
            AccountSettingsViewModel viewModel = new AccountSettingsViewModel();
            return View(viewModel);
        }

        [HttpGet("account")]
        public IActionResult AccountSettingsPartial()
        {
            return PartialView("_AccountSettings");
        }

        [HttpGet("email")]
        public IActionResult EmailSettingsPartial()
        {
            return PartialView("_EmailSettings");
        }

        [HttpGet("privacy")]
        public IActionResult PrivacySettingsPartial()
        {
            return PartialView("_PrivacySettings");
        }
        #endregion

        #region Profile Settings
        [HttpGet("profile-settings")]
        public IActionResult ProfileSettings()
        {
            AccountProfileSettingsViewModel viewModel = new AccountProfileSettingsViewModel();
            return View(viewModel);
        }

        [HttpGet("general")]
        public IActionResult GeneralSettingsPartial()
        {
            return PartialView("~/Views/Account/_GeneralSettings.cshtml");
        }

        [HttpGet("avatar")]
        public IActionResult AvatarSettingsPartial()
        {
            return PartialView("~/Views/Account/_AvatarSettings.cshtml");
        }

        [HttpGet("profile-background")]
        public IActionResult ProfileBackgroundSettingsPartial()
        {
            return PartialView("~/Views/Account/_ProfileBackgroundSettings.cshtml");
        }
        #endregion

        [HttpGet("edit/{id}")]
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

            AccountEditViewModel viewModel = new AccountEditViewModel
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
        public async Task<IActionResult> Edit(AccountEditViewModel viewModel)
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
            user.UserRoleID = viewModel.UserRoleID;

            userService.Update(user);
            return RedirectToAction("Index", user);
        }

        [HttpGet("delete/{id}")]
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
            return View("~/Views/Shared/AccessDenied.cshtml");
        }
    }
}
