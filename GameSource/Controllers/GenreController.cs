using System;
using System.Text;
using System.Collections.Generic;
using GameSource.Models;
using GameSource.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using GameSource.Services;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using GameSource.ViewModels.GenreViewModel;

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
            GenreIndexViewModel viewModel = new GenreIndexViewModel
            {
                Genres = genreService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            GenreDetailsViewModel viewModel = new GenreDetailsViewModel();
            viewModel.Genre = genreService.GetByID(id);
            return View(viewModel);
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
        public IActionResult Edit()
        {
            return View(new Genre());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Genre genre)
        {
            genreService.Update(genre);
            return RedirectToAction("Details", genre);
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