using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.ReviewCommentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers.GameSource
{
    [Route("review-comment")]
    public class ReviewCommentController : Controller
    {
        private IReviewCommentService reviewCommentService;

        public ReviewCommentController(IReviewCommentService reviewCommentService)
        {
            this.reviewCommentService = reviewCommentService;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewCommentCreateViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ReviewCommentEditViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ReviewCommentDeleteViewModel viewModel)
        {
            return View(viewModel);
        }
    }
}