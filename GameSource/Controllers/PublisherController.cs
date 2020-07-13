using System;
using System.Collections.Generic;
using System.Linq;
using GameSource.Models;
using GameSource.Services.Contracts;
using GameSource.ViewModels.PublisherViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers
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
            var publisherList = publisherService.GetAll();
            return View(publisherList);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Publisher publisher)
        {
            publisherService.Insert(publisher);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit()
        {
            return View(new Publisher());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Publisher publisher)
        {
            publisherService.Update(publisher);
            return RedirectToAction("Details", publisher);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            publisherService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}