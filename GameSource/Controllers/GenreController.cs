using System;
using System.Text;
using System.Collections.Generic;
using GameSource.Models;
using GameSource.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers
{
    public class GenreController : Controller
    {
        public IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var genreList = genreService.GetAll();
            return View(genreList);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var genre = genreService.GetByID(id);
            return View(genre);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Genre genre)
        {
            genreService.Insert(genre);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update()
        {
            return View(new Genre());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Genre genre)
        {
            genreService.Update(genre);
            return View("Details", genre);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id )
        {
            genreService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}