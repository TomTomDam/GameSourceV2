using System;
using System.Collections.Generic;
using System.Linq;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.PlatformTypeViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers.GameSource
{
    [Route("platform-type")]
    public class PlatformTypeController : Controller
    {
        public IPlatformTypeService platformTypeService;

        public PlatformTypeController(IPlatformTypeService platformTypeService)
        {
            this.platformTypeService = platformTypeService;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            PlatformTypeIndexViewModel viewModel = new PlatformTypeIndexViewModel
            {
                PlatformTypes = platformTypeService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PlatformType platformType = platformTypeService.GetByID((int)id);
            if (platformType == null)
            {
                return NotFound();
            }

            PlatformTypeDetailsViewModel viewModel = new PlatformTypeDetailsViewModel();
            viewModel.PlatformType = platformType;

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            PlatformTypeCreateViewModel viewModel = new PlatformTypeCreateViewModel();
            viewModel.PlatformType = new PlatformType();

            return View(viewModel);
        }

        [HttpPost("create")]
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


        [HttpGet("edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PlatformType platformType = platformTypeService.GetByID((int)id);
            if (platformType == null)
            {
                return NotFound();
            }

            PlatformTypeEditViewModel viewModel = new PlatformTypeEditViewModel();
            viewModel.PlatformType = platformType;

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PlatformTypeEditViewModel viewModel)
        {
            PlatformType platformType = platformTypeService.GetByID(viewModel.PlatformType.ID);

            platformType.Name = viewModel.PlatformType.Name;

            platformTypeService.Update(platformType);
            return RedirectToAction("Details", platformType);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PlatformType platformType = platformTypeService.GetByID((int)id);
            if (platformType == null)
            {
                return NotFound();
            }

            PlatformTypeDeleteViewModel viewModel = new PlatformTypeDeleteViewModel
            {
                PlatformType = platformType
            };

            return View(viewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PlatformTypeDeleteViewModel viewModel)
        {
            PlatformType platformType = platformTypeService.GetByID(viewModel.PlatformType.ID);

            platformTypeService.Delete(platformType.ID);
            return RedirectToAction("Index");
        }
    }
}