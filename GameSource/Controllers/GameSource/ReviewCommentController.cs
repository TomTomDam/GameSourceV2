using GameSource.Models.GameSource;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.ReviewCommentViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameSource.Controllers.GameSource
{
    [Route("review-comment")]
    public class ReviewCommentController : Controller
    {
        private IReviewCommentService reviewCommentService;
        private UserManager<User> userManager;

        public ReviewCommentController(IReviewCommentService reviewCommentService, UserManager<User> userManager)
        {
            this.reviewCommentService = reviewCommentService;
            this.userManager = userManager;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            ReviewCommentIndexViewModel viewModel = new ReviewCommentIndexViewModel()
            {
                ReviewComments = reviewCommentService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            if (id == 0)
                return NotFound();

            ReviewComment comment = reviewCommentService.GetByID(id);
            if (comment == null)
                return NotFound();

            Review commentReview = reviewCommentService.GetReview(comment);

            ReviewCommentDetailsViewModel viewModel = new ReviewCommentDetailsViewModel()
            {
                ReviewComment = comment,
                Review = commentReview
            };

            return View(comment);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            ReviewCommentCreateViewModel viewModel = new ReviewCommentCreateViewModel()
            {
                ReviewComment = new ReviewComment()
            };

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewCommentCreateViewModel viewModel)
        {
            ReviewComment comment = new ReviewComment()
            {
                ID = viewModel.ReviewComment.ID,
                Body = viewModel.ReviewComment.Body,
                DateCreated = DateTime.Now,
                CreatedByID = userManager.GetUserAsync(HttpContext.User).Result.Id,
                CreatedBy = userManager.GetUserAsync(HttpContext.User).Result,
                Review = viewModel.ReviewComment.Review
            };

            reviewCommentService.Insert(comment);

            return View(viewModel);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            if (id == 0)
                return NotFound();

            ReviewComment comment = reviewCommentService.GetByID(id);
            if (comment == null)
                return NotFound();

            Review commentReview = reviewCommentService.GetReview(comment);

            ReviewCommentEditViewModel viewModel = new ReviewCommentEditViewModel()
            {
                ReviewComment = comment,
                Review = commentReview
            };

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ReviewCommentEditViewModel viewModel)
        {
            ReviewComment comment = reviewCommentService.GetByID(viewModel.ReviewComment.ID);

            comment.Body = viewModel.ReviewComment.Body;

            reviewCommentService.Update(comment);

            return RedirectToAction("Details", comment);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            ReviewComment comment = reviewCommentService.GetByID(id);
            if (comment == null)
                return NotFound();

            Review commentReview = reviewCommentService.GetReview(comment);

            ReviewCommentDeleteViewModel viewModel = new ReviewCommentDeleteViewModel()
            {
                ReviewComment = comment,
                Review = commentReview
            };

            return View(viewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ReviewCommentDeleteViewModel viewModel)
        {
            ReviewComment comment = reviewCommentService.GetByID(viewModel.ReviewComment.ID);
            if (comment == null)
                return NotFound();

            reviewCommentService.Delete(comment.ID);

            return RedirectToAction("Index");
        }
    }
}