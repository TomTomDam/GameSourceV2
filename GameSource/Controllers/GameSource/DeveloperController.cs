using System;
using System.Collections.Generic;
using System.Linq;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.DeveloperViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers.GameSource
{
    public class DeveloperController : Controller
    {
        private IDeveloperService developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            this.developerService = developerService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            DeveloperIndexViewModel viewModel = new DeveloperIndexViewModel
            {
                Developers = developerService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            DeveloperDetailsViewModel viewModel = new DeveloperDetailsViewModel();
            viewModel.Developer = developerService.GetByID(id);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            DeveloperCreateViewModel viewModel = new DeveloperCreateViewModel();
            viewModel.Developer = new Developer();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DeveloperCreateViewModel viewModel)
        {
            Developer developer = new Developer
            {
                ID = viewModel.Developer.ID,
                Name = viewModel.Developer.Name
            };

            developerService.Insert(developer);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            DeveloperEditViewModel viewModel = new DeveloperEditViewModel();
            viewModel.Developer = developerService.GetByID(id);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DeveloperEditViewModel viewModel)
        {
            Developer developer = developerService.GetByID(viewModel.Developer.ID);

            developer.Name = viewModel.Developer.Name;

            developerService.Update(developer);
            return RedirectToAction("Details", developer);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            DeveloperDeleteViewModel viewModel = new DeveloperDeleteViewModel
            {
                Developer = developerService.GetByID(id)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(DeveloperDeleteViewModel viewModel)
        {
            Developer developer = developerService.GetByID(viewModel.Developer.ID);

            developerService.Delete(developer.ID);
            return RedirectToAction("Index");
        }
    }
}