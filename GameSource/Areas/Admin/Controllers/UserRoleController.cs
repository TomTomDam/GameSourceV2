using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Areas.Admin.ViewModels.UserRoleViewModel;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("admin/user-roles")]
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService userRoleService;
        private readonly RoleManager<UserRole> roleManager;

        public UserRoleController(IUserRoleService userRoleService, RoleManager<UserRole> roleManager) 
        {
            this.userRoleService = userRoleService;
            this.roleManager = roleManager;
        }

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
            AdminUserRoleIndexViewModel viewModel = new AdminUserRoleIndexViewModel
            {
                UserRoles = await userRoleService.GetAllAsync()
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

            var userRole = await userRoleService.GetByIDAsync((int)id);
            if (userRole == null)
            {
                return NotFound();
            }

            AdminUserRoleDetailsViewModel viewModel = new AdminUserRoleDetailsViewModel
            {
                UserRole = userRole
            };

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            AdminUserRoleCreateViewModel viewModel = new AdminUserRoleCreateViewModel();
            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminUserRoleCreateViewModel viewModel)
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
                    return RedirectToAction("Index");
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

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminUserRoleEditViewModel viewModel)
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
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
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

            return View(userRole);
        }

        [HttpPost("delete/{id}"), ActionName("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
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
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToAction("Index", roleManager.Roles);
        }

        [HttpGet("access-denied")]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Areas/Admin/Views/Shared/AccessDenied.cshtml");
        }

    }
}