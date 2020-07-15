using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Services.Contracts;
using GameSource.ViewModels.PlatformViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameSource.Controllers
{
    public class PlatformController : Controller
    {
        private IPlatformService platformService;
        private IPlatformTypeService platformTypeService;

        public PlatformController(IPlatformService platformService, IPlatformTypeService platformTypeService)
        {
            this.platformService = platformService;
            this.platformTypeService = platformTypeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            PlatformIndexViewModel viewModel = new PlatformIndexViewModel
            {
                Platforms = platformService.GetAll(),
                PlatformTypes = platformTypeService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var platform = platformService.GetByID(id);

            PlatformDetailsViewModel viewModel = new PlatformDetailsViewModel
            {
                Platform = platform,
                PlatformType = platformTypeService.GetByID(platform.PlatformTypeID)
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PlatformCreateViewModel viewModel = new PlatformCreateViewModel();
            viewModel.Platform = new Platform();
            viewModel.PlatformTypes = platformTypeService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlatformCreateViewModel viewModel)
        {
            Platform platform = new Platform
            {
                ID = viewModel.Platform.ID,
                Name = viewModel.Platform.Name,
                PlatformTypeID = viewModel.Platform.PlatformTypeID
            };

            platformService.Insert(platform);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PlatformEditViewModel viewModel = new PlatformEditViewModel();
            viewModel.Platform = platformService.GetByID(id);
            viewModel.PlatformTypes = platformTypeService.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PlatformEditViewModel viewModel)
        {
            Platform platform = platformService.GetByID(viewModel.Platform.ID);

            platform.Name = viewModel.Platform.Name;
            platform.PlatformTypeID = viewModel.Platform.PlatformTypeID;

            platformService.Update(platform);
            return RedirectToAction("Details", platform);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var platform = platformService.GetByID(id);

            PlatformDeleteViewModel viewModel = new PlatformDeleteViewModel
            {
                Platform = platform,
                PlatformType = platformTypeService.GetByID(platform.PlatformTypeID)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(PlatformDeleteViewModel viewModel)
        {
            Platform platform = platformService.GetByID(viewModel.Platform.ID);

            platformService.Delete(platform.ID);
            return RedirectToAction("Index");
        }
    }
}