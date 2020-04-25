using GameSource.Data.Repositories;
using GameSource.Models;
using GameSource.Services;
using GameSource.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Controllers
{
    public class GamesController : Controller
    {
        private IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public IActionResult Index()
        {
            var gamesList = gameService.GetAll();
            return View(gamesList);
        }

        public IActionResult Page(int id)
        {
            var game = gameService.GetByID(id);
            return View(game);
        }

        public IActionResult Create(Game game)
        {
            gameService.Insert(game);
            return RedirectToAction("Index");
        }

        public IActionResult Update(Game game)
        {
            gameService.Update(game);
            return RedirectToAction("Page", game);
        }

        public IActionResult Delete(int id)
        {
            gameService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
