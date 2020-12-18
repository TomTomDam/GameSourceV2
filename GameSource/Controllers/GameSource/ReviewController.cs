﻿using GameSource.Models.GameSource;
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

            IEnumerable<ReviewComment> reviewComments = reviewService.GetReviewComments(review);

            ReviewDetailsViewModel viewModel = new ReviewDetailsViewModel()
            {
                Review = review,
                ReviewComments = reviewComments
            };

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            ReviewCreateViewModel viewModel = new ReviewCreateViewModel()
            {
                Review = new Review(),
                User = userManager.GetUserAsync(HttpContext.User).Result
            };

            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewCreateViewModel viewModel)
        {
            Review review = new Review()
            {
                ID = viewModel.Review.ID,
                Title = viewModel.Review.Title,
                Body = viewModel.Review.Body,
                DateCreated = DateTime.Now,
                DateModified = null,
                Rating = 0,
                Helpful = 0,
                CreatedByID = viewModel.User.Id,
                CreatedBy = viewModel.User,
                ReviewComments = new List<ReviewComment>() ?? null,
                //GameID = 0,
                //Game = null
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

            IEnumerable<ReviewComment> reviewComments = reviewService.GetReviewComments(review);

            ReviewEditViewModel viewModel = new ReviewEditViewModel()
            {
                Review = review,
                ReviewComments = reviewComments
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

            IEnumerable<ReviewComment> reviewComments = reviewService.GetReviewComments(review);

            ReviewDeleteViewModel viewModel = new ReviewDeleteViewModel()
            {
                Review = review,
                ReviewComments = reviewComments
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