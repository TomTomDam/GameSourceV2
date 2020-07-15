using System;
using System.Linq;
using System.Text;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.GameViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameSource.Controllers.GameSource
{
    public class GamesController : Controller
    {
        private IGameService gameService;
        private IGenreService genreService;
        private IDeveloperService developerService;
        private IPublisherService publisherService;
        private IPlatformService platformService;

        public GamesController(IGameService gameService, IGenreService genreService, IDeveloperService developerService, IPublisherService publisherService, IPlatformService platformService)
        {
            this.gameService = gameService;
            this.genreService = genreService;
            this.developerService = developerService;
            this.publisherService = publisherService;
            this.platformService = platformService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            GameIndexViewModel viewModel = new GameIndexViewModel
            {
                Games = gameService.GetAll(),
                Genres = genreService.GetAll(),
                Developers = developerService.GetAll(),
                Publishers = publisherService.GetAll(),
                Platforms = platformService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var game = gameService.GetByID(id);

            GameDetailsViewModel viewModel = new GameDetailsViewModel
            {
                Game = game,
                Genre = genreService.GetByID(game.GenreID),
                Developer = developerService.GetByID(game.DeveloperID),
                Publisher = publisherService.GetByID(game.PublisherID),
                Platform = platformService.GetByID(game.PlatformID)
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            GameCreateViewModel viewModel = new GameCreateViewModel();
            viewModel.Game = new Game();
            viewModel.Genres = genreService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.Developers = developerService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.Publishers = publisherService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.Platforms = platformService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameCreateViewModel viewModel)
        {
            Game game = new Game
            {
                ID = viewModel.Game.ID,
                Name = viewModel.Game.Name,
                Description = viewModel.Game.Description,
                GenreID = viewModel.Game.GenreID,
                DeveloperID = viewModel.Game.DeveloperID,
                PublisherID = viewModel.Game.PublisherID,
                PlatformID = viewModel.Game.PlatformID
            };

            gameService.Insert(game);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            GameEditViewModel viewModel = new GameEditViewModel();
            viewModel.Game = gameService.GetByID(id);
            viewModel.Genres = genreService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.Developers = developerService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.Publishers = publisherService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.Platforms = platformService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GameEditViewModel viewModel)
        {
            Game game = gameService.GetByID(viewModel.Game.ID);

            game.Name = viewModel.Game.Name;
            game.Description = viewModel.Game.Description;
            game.GenreID = viewModel.Game.GenreID;
            game.DeveloperID = viewModel.Game.DeveloperID;
            game.PublisherID = viewModel.Game.PublisherID;
            game.PlatformID = viewModel.Game.PlatformID;

            gameService.Update(game);
            return RedirectToAction("Details", game);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var game = gameService.GetByID(id);

            GameDeleteViewModel viewModel = new GameDeleteViewModel
            {
                Game = gameService.GetByID(id),
                Genre = genreService.GetByID(game.GenreID),
                Developer = developerService.GetByID(game.DeveloperID),
                Publisher = publisherService.GetByID(game.PublisherID),
                Platform = platformService.GetByID(game.PlatformID)
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(GameDeleteViewModel viewModel)
        {
            Game game = gameService.GetByID(viewModel.Game.ID);

            gameService.Delete(game.ID);
            return RedirectToAction("Index");
        }
    }
}
