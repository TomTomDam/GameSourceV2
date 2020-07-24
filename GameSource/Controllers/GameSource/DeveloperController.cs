using System;
using System.Collections.Generic;
using System.Linq;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.DeveloperViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers.GameSource
{
    [Route("developer")]
    public class DeveloperController : Controller
    {
        private IDeveloperService developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            this.developerService = developerService;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            DeveloperIndexViewModel viewModel = new DeveloperIndexViewModel
            {
                Developers = developerService.GetAll()
            };

            return View("Index", viewModel);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeveloperDetailsViewModel viewModel = new DeveloperDetailsViewModel();
            viewModel.Developer = developerService.GetByID((int)id);

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            DeveloperCreateViewModel viewModel = new DeveloperCreateViewModel();
            viewModel.Developer = new Developer();

            return View(viewModel);
        }

        [HttpPost("create")]
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

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DeveloperEditViewModel viewModel = new DeveloperEditViewModel();
            viewModel.Developer = developerService.GetByID((int)id);

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DeveloperEditViewModel viewModel)
        {
            Developer developer = developerService.GetByID(viewModel.Developer.ID);

            developer.Name = viewModel.Developer.Name;

            developerService.Update(developer);
            return RedirectToAction("Details", developer);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Developer developer = developerService.GetByID((int)id);
            if (developer == null)
            {
                return NotFound();
            }

            DeveloperDeleteViewModel viewModel = new DeveloperDeleteViewModel
            {
                Developer = developer
            };

            return View(viewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DeveloperDeleteViewModel viewModel)
        {
            Developer developer = developerService.GetByID(viewModel.Developer.ID);

            developerService.Delete(developer.ID);
            return RedirectToAction("Index");
        }
    }
}