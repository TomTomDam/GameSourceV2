using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.NewsArticleCategoryViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers.GameSource
{
    [Route("news-article-category")]
    [Authorize(Roles = "Admin")]
    public class NewsArticleCategoryController : Controller
    {
        private readonly INewsArticleCategoryService newsArticleCategoryService;

        public NewsArticleCategoryController(INewsArticleCategoryService newsArticleCategoryService)
        {
            this.newsArticleCategoryService = newsArticleCategoryService;
        }

        [HttpGet("index")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            NewsArticleCategoryIndexViewModel viewModel = new NewsArticleCategoryIndexViewModel
            {
                NewsArticleCategories = newsArticleCategoryService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet("details/{id}")]
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            NewsArticleCategory category = newsArticleCategoryService.GetByID((int)id);
            if (category == null)
                return NotFound();

            NewsArticleCategoryDetailsViewModel viewModel = new NewsArticleCategoryDetailsViewModel
            {
                NewsArticleCategory = category
            };

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            NewsArticleCategoryCreateViewModel viewModel = new NewsArticleCategoryCreateViewModel
            {
                NewsArticleCategory = new NewsArticleCategory()
            };

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewsArticleCategoryCreateViewModel viewModel)
        {
            NewsArticleCategory category = new NewsArticleCategory
            {
                ID = viewModel.NewsArticleCategory.ID,
                Name = viewModel.NewsArticleCategory.Name
            };

            newsArticleCategoryService.Insert(category);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            NewsArticleCategory category = newsArticleCategoryService.GetByID((int)id);
            if (category == null)
                return NotFound();

            NewsArticleCategoryEditViewModel viewModel = new NewsArticleCategoryEditViewModel
            {
                NewsArticleCategory = category
            };

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NewsArticleCategoryEditViewModel viewModel)
        {
            NewsArticleCategory category = new NewsArticleCategory
            {
                ID = viewModel.NewsArticleCategory.ID,
                Name = viewModel.NewsArticleCategory.Name
            };

            newsArticleCategoryService.Update(category);
            return RedirectToAction("Details", viewModel);
        }

        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            NewsArticleCategory category = newsArticleCategoryService.GetByID((int)id);
            if (category == null)
                return NotFound();

            NewsArticleCategoryDeleteViewModel viewModel = new NewsArticleCategoryDeleteViewModel
            {
                NewsArticleCategory = category
            };

            return View(viewModel);
        }

        [HttpPost("delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(NewsArticleCategoryDeleteViewModel viewModel)
        {
            NewsArticleCategory category = newsArticleCategoryService.GetByID(viewModel.NewsArticleCategory.ID);
            if (category == null)
                return NotFound();

            newsArticleCategoryService.Delete(category.ID);
            return RedirectToAction("Index");
        }
    }
}