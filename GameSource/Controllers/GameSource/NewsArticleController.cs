using GameSource.Models.GameSource;
using GameSource.Models.GameSourceUser;
using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.NewsArticleViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameSource.Controllers.GameSource
{
    [Route("news-article")]
    [Authorize(Roles = "Admin")]
    public class NewsArticleController : Controller
    {
        private readonly INewsArticleService newsArticleService;
        private readonly UserManager<User> userManager;

        public NewsArticleController(INewsArticleService newsArticleService, UserManager<User> userManager)
        {
            this.newsArticleService = newsArticleService;
            this.userManager = userManager;
        }

        [HttpGet("index")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            NewsArticleIndexViewModel viewModel = new NewsArticleIndexViewModel
            {
                NewsArticles = newsArticleService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet("details/{id}")]
        [AllowAnonymous]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsArticleDetailsViewModel viewModel = new NewsArticleDetailsViewModel();
            viewModel.NewsArticle = newsArticleService.GetByID((int)id);

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            NewsArticleCreateViewModel viewModel = new NewsArticleCreateViewModel();
            viewModel.NewsArticle = new NewsArticle();

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        
        public IActionResult Create(NewsArticleCreateViewModel viewModel)
        {
            NewsArticle newsArticle = new NewsArticle
            {
                ID = viewModel.NewsArticle.ID,
                Title = viewModel.NewsArticle.Title,
                Body = viewModel.NewsArticle.Body,
                DateCreated = DateTime.Now,
                DateModified = null,
                CreatedByID = userManager.GetUserAsync(HttpContext.User).Result.Id,
                CreatedBy = userManager.GetUserAsync(HttpContext.User).Result
            };

            newsArticleService.Insert(newsArticle);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsArticleEditViewModel viewModel = new NewsArticleEditViewModel();
            viewModel.NewsArticle = newsArticleService.GetByID((int)id);

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NewsArticleEditViewModel viewModel)
        {
            NewsArticle newsArticle = newsArticleService.GetByID(viewModel.NewsArticle.ID);

            newsArticle.Title = viewModel.NewsArticle.Title;
            newsArticle.Body = viewModel.NewsArticle.Body;
            newsArticle.DateModified = DateTime.Now;

            newsArticleService.Update(newsArticle);
            return RedirectToAction("Details", newsArticle);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsArticle newsArticle = newsArticleService.GetByID((int)id);
            if (newsArticle == null)
            {
                return NotFound();
            }

            NewsArticleDeleteViewModel viewModel = new NewsArticleDeleteViewModel
            {
                NewsArticle = newsArticle
            };

            return View(viewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(NewsArticleDeleteViewModel viewModel)
        {
            NewsArticle newsArticle = newsArticleService.GetByID(viewModel.NewsArticle.ID);

            newsArticleService.Delete(newsArticle.ID);
            return RedirectToAction("Index");
        }
    }
}