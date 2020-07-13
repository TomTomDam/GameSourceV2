using System;
using System.Collections.Generic;
using System.Linq;
using GameSource.Models;
using GameSource.Services.Contracts;
using GameSource.ViewModels.DeveloperViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Developer developer)
        {
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
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            developerService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}