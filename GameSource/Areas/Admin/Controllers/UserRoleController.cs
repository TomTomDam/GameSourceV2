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
    [Route("Admin/[controller]")]
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService userRoleService;
        private readonly RoleManager<UserRole> roleManager;

        public UserRoleController(IUserRoleService userRoleService, RoleManager<UserRole> roleManager) 
        {
            this.userRoleService = userRoleService;
            this.roleManager = roleManager;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            AdminUserRoleIndexViewModel viewModel = new AdminUserRoleIndexViewModel
            {
                UserRoles = await userRoleService.GetAllAsync()
            };

            return View("~/Areas/Admin/Views/UserRole/Index.cshtml", viewModel);
        }

        [HttpGet("Details/{id}")]
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

            return View("~/Areas/Admin/Views/UserRole/Details.cshtml", viewModel);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            AdminUserRoleCreateViewModel viewModel = new AdminUserRoleCreateViewModel();
            return View("~/Areas/Admin/Views/UserRole/Create.cshtml", viewModel);
        }

        [HttpPost("Create")]
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
                    return RedirectToAction("UserRoleIndex");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("~/Areas/Admin/Views/UserRole/Create.cshtml", viewModel);
        }

        [HttpGet("Edit/{id}")]
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

            return View("~/Areas/Admin/Views/UserRole/Edit.cshtml", viewModel);
        }

        [HttpPost("Edit/{id}")]
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
            return RedirectToAction("UserRoleIndex");
        }

        [HttpGet("Delete/{id}")]
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

            return View("~/Areas/Admin/Views/UserRole/Delete.cshtml", userRole);
        }

        [HttpPost("Delete/{id}")]
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
                    return RedirectToAction("UserRoleIndex");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToAction("UserRoleIndex", roleManager.Roles);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("~/Areas/Admin/Views/Shared/AccessDenied.cshtml");
        }

    }
}