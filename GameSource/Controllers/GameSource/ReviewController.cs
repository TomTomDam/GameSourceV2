using GameSource.Models.GameSource;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.ReviewViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GameSource.Controllers.GameSource
{
    [Route("game/review")]
    public class ReviewController : Controller
    {
        private IReviewService reviewService;
        private IGameService gameService;
        private UserManager<User> userManager;

        public ReviewController(
            IReviewService reviewService, 
            IGameService gameService,
            UserManager<User> userManager)
        {
            this.reviewService = reviewService;
            this.gameService = gameService;
            this.userManager = userManager;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            ReviewIndexViewModel viewModel = new ReviewIndexViewModel()
            {
                Reviews = reviewService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            if (id == 0)
                return NotFound();

            Review review = reviewService.GetByID(id);
            if (review == null)
                return NotFound();

            ReviewDetailsViewModel viewModel = new ReviewDetailsViewModel()
            {
                Review = review,
                ReviewComments = review.ReviewComments
            };

            return View(viewModel);
        }

        [HttpGet("{gameId}/create")]
        public IActionResult Create(int gameId)
        {
            if (gameId == 0)
                return NotFound();

            ReviewCreateViewModel viewModel = new ReviewCreateViewModel()
            {
                Review = new Review(),
                Game = gameService.GetByID(gameId)
            };

            return PartialView("_Create", viewModel);
        }

        [HttpPost("{gameId}/create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewCreateViewModel viewModel)
        {
            User signedInUser = userManager.GetUserAsync(HttpContext.User).Result;

            if (viewModel.Game.ID == 0)
                return NotFound();
            Game reviewedGame = gameService.GetByID(viewModel.Game.ID);

            Review review = new Review()
            {
                ID = viewModel.Review.ID,
                Title = viewModel.Review.Title,
                Body = viewModel.Review.Body,
                DateCreated = DateTime.Now,
                DateModified = null,
                Rating = 0,
                Helpful = 0,
                CreatedByID = signedInUser.Id,
                CreatedBy = signedInUser,
                ReviewComments = new List<ReviewComment>() ?? null,
                GameID = reviewedGame.ID,
                Game = reviewedGame
            };

            reviewService.Insert(review);

            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            if (id == 0)
                return NotFound();

            Review review = reviewService.GetByID(id);
            if (review == null)
                return NotFound();

            ReviewEditViewModel viewModel = new ReviewEditViewModel()
            {
                Review = review,
                ReviewComments = review.ReviewComments
            };

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ReviewEditViewModel viewModel)
        {
            Review review = reviewService.GetByID(viewModel.Review.ID);
            if (review == null)
                return NotFound();

            review.Body = viewModel.Review.Body;
            review.DateModified = DateTime.Now;

            reviewService.Update(review);

            return RedirectToAction("Details", review);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Review review = reviewService.GetByID(id);
            if (review == null)
                return NotFound();

            ReviewDeleteViewModel viewModel = new ReviewDeleteViewModel()
            {
                Review = review,
                ReviewComments = review.ReviewComments
            };

            return View(viewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ReviewDeleteViewModel viewModel)
        {
            Review review = reviewService.GetByID(viewModel.Review.ID);
            if (review == null)
                return NotFound();

            reviewService.Delete(review.ID);

            return RedirectToAction("Index");
        }
    }
}