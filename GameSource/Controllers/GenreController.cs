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
            GenreCreateViewModel viewModel = new GenreCreateViewModel();
            viewModel.Genre = new Genre();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenreCreateViewModel viewModel)
        {
            Genre genre = new Genre
            {
                ID = viewModel.Genre.ID,
                Name = viewModel.Genre.Name
            };

            genreService.Insert(genre);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            GenreEditViewModel viewModel = new GenreEditViewModel();
            viewModel.Genre = genreService.GetByID(id);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GenreEditViewModel viewModel)
        {
            Genre Genre = genreService.GetByID(viewModel.Genre.ID);

            Genre.Name = viewModel.Genre.Name;

            genreService.Update(Genre);
            return RedirectToAction("Details", Genre);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            GenreDeleteViewModel viewModel = new GenreDeleteViewModel
            {
                Genre = genreService.GetByID(id)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(GenreDeleteViewModel viewModel)
        {
            Genre genre = genreService.GetByID(viewModel.Genre.ID);

            genreService.Delete(genre.ID);
            return RedirectToAction("Index");
        }
    }
}