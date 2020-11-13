using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Identity;
using GameSource.ViewModels.GameSourceUser.AccountViewModel;
using GameSource.Models.GameSourceUser.Enums;
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

                //Create a new UserProfile - do not assign User to UserProfile until User is successfully created
                UserProfile userProfile = new UserProfile
                {
                    DisplayName = null,
                    Biography = null,
                    UserProfileVisibilityID = (int)UserProfileVisibilityEnum.Everyone,
                    UserProfileCommentPermissionID = (int)UserProfileCommentPermissionEnum.Everyone,
                };

                //Add a default UserProfile Avatar Image
                string defaultAvatarImageFileName = "default_avatar.png";
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images\\UserProfile\\Avatar", defaultAvatarImageFileName);

                userProfile.AvatarFilePath = defaultAvatarImageFileName;

                var result = await userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    User newUser = await userManager.FindByNameAsync(user.UserName);
                    var roleResult = await userManager.AddToRoleAsync(newUser, "Member");

                    if (roleResult.Succeeded)
                    {
                        //Assign and create new UserProfile after user is created
                        userProfile.UserID = user.Id;
                        await userProfileService.InsertAsync(userProfile);

                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Home");
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

        [HttpGet("access-denied")]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }
    }
}
