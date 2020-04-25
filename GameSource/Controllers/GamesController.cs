using GameSource.Data.Repositories;
using GameSource.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.Controllers
{
    public class GamesController : Controller
    {
        protected GameRepository gamesRepo;

        public GamesController(GameRepository gamesRepo)
        {
            this.gamesRepo = gamesRepo;
        }

        public IActionResult Index()
        {
            var gamesList = gamesRepo.GetAll();
            return View(gamesList);
        }

        public IActionResult Page(int id)
        {
            var game = gamesRepo.GetByID(id);
            return View(game);
        }

        public IActionResult Create(Game game)
        {
            gamesRepo.Insert(game);
            return RedirectToAction("Index");
        }

        public IActionResult Update(Game game)
        {
            gamesRepo.Update(game);
            return RedirectToAction("Page", game);
        }

        public IActionResult Delete(int id)
        {
            gamesRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
