using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers.GameSource
{
    [Route("news-article")]
    public class NewsArticleController : Controller
    {
        public NewsArticleController()
        {

        }

        [HttpGet("index")]
        public IActionResult Index(ViewResult viewModel)
        {
            return View();
        }

        [HttpGet("details")]
        public IActionResult Details(ViewResult viewModel)
        {
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create(ViewResult viewModel)
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(ViewResult viewModel)
        {
            return View();
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id)
        {
            return View();
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(ViewResult viewModel)
        {
            return View();
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            return View();
        }
    }
}