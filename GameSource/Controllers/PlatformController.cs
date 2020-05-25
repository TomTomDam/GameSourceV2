using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers
{
    public class PlatformController : Controller
    {
        private IPlatformService platformService;

        public PlatformController(IPlatformService platformService)
        {
            this.platformService = platformService;
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
            var platform = platformService.GetByID(id);
            return View(platform);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Platform platform)
        {
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