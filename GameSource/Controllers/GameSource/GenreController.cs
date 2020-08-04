using System;
using System.Text;
using System.Collections.Generic;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Mvc;
using GameSource.ViewModels.GameSource.GenreViewModel;
using System.Threading.Tasks;
using System.Linq;

namespace GameSource.Controllers.GameSource
{
    [Route("genre")]
    public class GenreController : Controller
    {
        public IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet("get")]
        public async Task<IList<Genre>> Get(string filter)
        {
            List<Genre> models = await genreService.FindByName(filter);

            var viewModels = models
                .Select(x => x.ToViewModel())
                .ToList();

            return viewModels;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            GenreIndexViewModel viewModel = new GenreIndexViewModel
            {
                Genres = genreService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet("GetAll")]
        public IEnumerable<Genre> GetAll()
        {
            return genreService.GetAll();
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Genre genre = genreService.GetByID((int)id);
            if (genre == null)
            {
                return NotFound();
            }

            GenreDetailsViewModel viewModel = new GenreDetailsViewModel();
            viewModel.Genre = genre;

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            GenreCreateViewModel viewModel = new GenreCreateViewModel();
            viewModel.Genre = new Genre();

            return View(viewModel);
        }

        [HttpPost("create")]
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

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Genre genre = genreService.GetByID((int)id);
            if (genre == null)
            {
                return NotFound();
            }

            GenreEditViewModel viewModel = new GenreEditViewModel();
            viewModel.Genre = genre;

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GenreEditViewModel viewModel)
        {
            Genre Genre = genreService.GetByID(viewModel.Genre.ID);

            Genre.Name = viewModel.Genre.Name;

            genreService.Update(Genre);
            return RedirectToAction("Details", Genre);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Genre genre = genreService.GetByID((int)id);
            if (genre == null)
            {
                return NotFound();
            }

            GenreDeleteViewModel viewModel = new GenreDeleteViewModel
            {
                Genre = genre
            };

            return View(viewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(GenreDeleteViewModel viewModel)
        {
            Genre genre = genreService.GetByID(viewModel.Genre.ID);

            genreService.Delete(genre.ID);
            return RedirectToAction("Index");
        }
    }
}