using System;
using System.Text;
using GameSource.Models;
using GameSource.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers
{
    public class GamesController : Controller
    {
        private IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var gamesList = gameService.GetAll();
            return View(gamesList);
        }

        [HttpGet]
        public IActionResult Game(int id)
        {
            var game = gameService.GetByID(id);
            return View(game);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Game game)
        {
            gameService.Insert(game);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Update()
        {
            return View(new Game());
        }

        [HttpPost]
        public IActionResult Update(Game game)
        {
            gameService.Update(game);
            return RedirectToAction("Page", game);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            gameService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
