using System;
using System.Collections.Generic;
using System.Linq;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.PublisherViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers.GameSource
{
    public class PublisherController : Controller
    {
        private IPublisherService publisherService;
        public PublisherController(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            PublisherIndexViewModel viewModel = new PublisherIndexViewModel
            {
                Publishers = publisherService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            PublisherDetailsViewModel viewModel = new PublisherDetailsViewModel();
            viewModel.Publisher = publisherService.GetByID(id);

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PublisherCreateViewModel viewModel = new PublisherCreateViewModel();
            viewModel.Publisher = new Publisher();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PublisherCreateViewModel viewModel)
        {
            Publisher publisher = new Publisher
            {
                ID = viewModel.Publisher.ID,
                Name = viewModel.Publisher.Name
            };

            publisherService.Insert(publisher);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            PublisherEditViewModel viewModel = new PublisherEditViewModel();
            viewModel.Publisher = publisherService.GetByID(id);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PublisherEditViewModel viewModel)
        {
            Publisher publisher = publisherService.GetByID(viewModel.Publisher.ID);

            publisher.Name = viewModel.Publisher.Name;

            publisherService.Update(publisher);
            return RedirectToAction("Details", publisher);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            PublisherDeleteViewModel viewModel = new PublisherDeleteViewModel
            {
                Publisher = publisherService.GetByID(id)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(PublisherDeleteViewModel viewModel)
        {
            Publisher publisher = publisherService.GetByID(viewModel.Publisher.ID);

            publisherService.Delete(publisher.ID);
            return RedirectToAction("Index");
        }
    }
}