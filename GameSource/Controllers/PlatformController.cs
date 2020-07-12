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
            var platformList = platformService.GetAll();
            return View(platformList);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            PlatformDetailsViewModel viewModel = new PlatformDetailsViewModel();
            viewModel.Platform = platformService.GetByID(id);
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
                PlatformType = viewModel.Platform.PlatformType
            };
            platformService.Insert(platform);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Update()
        {
            return View(new Platform());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Platform platform)
        {
            platformService.Update(platform);
            return RedirectToAction("Details", platform);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            platformService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}