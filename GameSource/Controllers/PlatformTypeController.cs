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
            var platformTypeList = platformTypeService.GetAll();
            return View(platformTypeList);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlatformType platformType)
        {
            platformTypeService.Insert(platformType);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Update()
        {
            return View(new PlatformType());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(PlatformType platformType)
        {
            platformTypeService.Update(platformType);
            return RedirectToAction("Details", platformType);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            platformTypeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}