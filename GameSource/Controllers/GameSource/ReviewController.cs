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
    [Route("review")]
    public class ReviewController : Controller
    {
        private IReviewService reviewService;
        private UserManager<User> userManager;

        public ReviewController(IReviewService reviewService, UserManager<User> userManager)
        {
            this.reviewService = reviewService;
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
                Review = review
            };

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            ReviewCreateViewModel viewModel = new ReviewCreateViewModel()
            {
                Review = new Review()
            };

            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewCreateViewModel viewModel)
        {
            Review review = new Review()
            {
                Title = viewModel.Review.Title,
                Body = viewModel.Review.Body,
                DateCreated = DateTime.Now,
                DateModified = null,
                Rating = 0,
                CreatedByID = userManager.GetUserAsync(HttpContext.User).Result.Id,
                CreatedBy = userManager.GetUserAsync(HttpContext.User).Result,
                ReviewComments = new List<ReviewComment>() ?? null
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
                Review = review
            };

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ReviewEditViewModel viewModel)
        {
            Review review = reviewService.GetByID(viewModel.Review.Id);
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
                Review = review
            };

            return View(viewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ReviewDeleteViewModel viewModel)
        {
            Review review = reviewService.GetByID(viewModel.Review.Id);
            if (review == null)
                return NotFound();

            reviewService.Delete(review.Id);

            return RedirectToAction("Index");
        }
    }
}