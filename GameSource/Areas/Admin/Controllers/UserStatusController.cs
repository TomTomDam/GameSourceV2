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
    [Route("admin/user-statuses")]
    public class UserStatusController : Controller
    {
        private readonly IUserStatusService userStatusService;

        public UserStatusController(IUserStatusService userStatusService)
        {
            this.userStatusService = userStatusService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            AdminUserStatusIndexViewModel viewModel = new AdminUserStatusIndexViewModel
            {
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

            AdminUserStatusDetailsViewModel viewModel = new AdminUserStatusDetailsViewModel();
            viewModel.UserStatus = await userStatusService.GetByIDAsync((int)id);

            return View(viewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            AdminUserStatusCreateViewModel viewModel = new AdminUserStatusCreateViewModel();

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminUserStatusCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserStatus userStatus = new UserStatus
                {
                    Name = viewModel.Name
                };

                await userStatusService.InsertAsync(userStatus);
                return RedirectToAction("Index");
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

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
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
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
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

            return View(viewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AdminUserStatusDeleteViewModel viewModel)
        {
            UserStatus userStatus = await userStatusService.GetByIDAsync(viewModel.ID);
            if (userStatus != null)
            {
                await userStatusService.DeleteAsync(userStatus.Id);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet("access-denied")]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Areas/Admin/Views/Shared/AccessDenied.cshtml");
        }

    }
}