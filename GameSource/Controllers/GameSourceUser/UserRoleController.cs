using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSourceUser.Contracts;
using Microsoft.AspNetCore.Identity;
using GameSource.ViewModels.GameSourceUser.UserRoleViewModel;
using Microsoft.AspNetCore.Authorization;

namespace GameSource.Controllers.GameSourceUser
{
    //[Authorize(Roles = "Admin")]
    public class UserRoleController : Controller
    {
        private readonly IUserRoleService userRoleService;
        private readonly RoleManager<UserRole> roleManager;

        public UserRoleController(IUserRoleService userRoleService, RoleManager<UserRole> roleManager)
        {
            this.userRoleService = userRoleService;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UserRoleIndexViewModel viewModel = new UserRoleIndexViewModel
            {
                UserRoles = await userRoleService.GetAllAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
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

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            UserRoleCreateViewModel viewModel = new UserRoleCreateViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRoleCreateViewModel viewModel)
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRoleEditViewModel viewModel)
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
            return RedirectToAction("Index", userRole);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userRole = await roleManager.FindByIdAsync(id.ToString());
            if (userRole == null)
            {
                return NotFound();
            }

            return View(userRole);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
