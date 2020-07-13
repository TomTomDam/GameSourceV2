using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Services.Contracts;
using GameSource.ViewModels.PlatformTypeViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers
{
    public class PlatformTypeController : Controller
    {
        public IPlatformTypeService platformTypeService;

        public PlatformTypeController(IPlatformTypeService platformTypeService)
        {
            this.platformTypeService = platformTypeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            PlatformTypeIndexViewModel viewModel = new PlatformTypeIndexViewModel
            {
                PlatformTypes = platformTypeService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            PlatformTypeDetailsViewModel viewModel = new PlatformTypeDetailsViewModel();
            viewModel.PlatformType = platformTypeService.GetByID(id);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PlatformTypeCreateViewModel viewModel = new PlatformTypeCreateViewModel();
            viewModel.PlatformType = new PlatformType();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlatformTypeCreateViewModel viewModel)
        {
            PlatformType platformType = new PlatformType
            {
                ID = viewModel.PlatformType.ID,
                Name = viewModel.PlatformType.Name
            };

            platformTypeService.Insert(platformType);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            PlatformTypeEditViewModel viewModel = new PlatformTypeEditViewModel();
            viewModel.PlatformType = platformTypeService.GetByID(id);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PlatformTypeEditViewModel viewModel)
        {
            PlatformType platformType = platformTypeService.GetByID(viewModel.PlatformType.ID);

            platformType.Name = viewModel.PlatformType.Name;

            platformTypeService.Update(platformType);
            return RedirectToAction("Details", platformType);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            PlatformTypeDeleteViewModel viewModel = new PlatformTypeDeleteViewModel
            {
                PlatformType = platformTypeService.GetByID(id)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(PlatformTypeDeleteViewModel viewModel)
        {
            PlatformType platformType = platformTypeService.GetByID(viewModel.PlatformType.ID);

            platformTypeService.Delete(platformType.ID);
            return RedirectToAction("Index");
        }
    }
}