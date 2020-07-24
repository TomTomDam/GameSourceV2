using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Areas.Admin.ViewModels.UserStatusViewModel;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class UserStatusController : Controller
    {
        private readonly IUserStatusService userStatusService;

        public UserStatusController(IUserStatusService userStatusService)
        {
            this.userStatusService = userStatusService;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            AdminUserStatusIndexViewModel viewModel = new AdminUserStatusIndexViewModel
            {
                UserStatuses = await userStatusService.GetAllAsync()
            };

            return View("~/Areas/Admin/Views/UserStatus/Index.cshtml", viewModel);
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AdminUserStatusDetailsViewModel viewModel = new AdminUserStatusDetailsViewModel();
            viewModel.UserStatus = await userStatusService.GetByIDAsync((int)id);

            return View("~/Areas/Admin/Views/UserStatus/Details.cshtml", viewModel);
        }

        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            AdminUserStatusCreateViewModel viewModel = new AdminUserStatusCreateViewModel();
            return View("~/Areas/Admin/Views/UserStatus/Create.cshtml", viewModel);
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminUserStatusCreateViewModel viewModel)
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

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
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

        [HttpPost("Edit/{id}")]
        public async Task<IActionResult> Edit(AdminUserStatusEditViewModel viewModel)
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

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AdminUserStatusDeleteViewModel viewModel)
        {
            UserStatus userStatus = await userStatusService.GetByIDAsync(viewModel.ID);
            if (userStatus != null)
            {
                await userStatusService.DeleteAsync(userStatus.Id);
                return RedirectToAction("UserStatusIndex");
            }

            return View("~/Areas/Admin/Views/UserStatus/Delete.cshtml");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Areas/Admin/Views/Shared/AccessDenied.cshtml");
        }

    }
}