using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameSource.API.Controllers
{
    [Route("api/news-article-category")]
    [ApiController]
    public class NewsArticleCategoryController : ControllerBase
    {
        private readonly NewsArticleCategoryService newsArticleCategoryService;

        public NewsArticleCategoryController(NewsArticleCategoryService newsArticleCategoryService)
        {
            this.newsArticleCategoryService = newsArticleCategoryService;
        }

        [HttpGet("GetAll")]
        public IEnumerable<NewsArticleCategory> GetAll()
        {
            return newsArticleCategoryService.GetAll();
        }

        //[HttpGet("GetAllAsync")]
        //public async Task<ApiResponse> GetAllAsync()
        //{
        //    var result = await newsArticleCategoryService.GetAllAsync();

        //    if (result.Any())
        //        return new ApiResponse(ResponseStatusCode.Success, "Successfully returned Users list.");

        //    return new ApiResponse(ResponseStatusCode.Error, "Could not return Users list.");
        //}

        [HttpGet("GetAllAsync")]
        public async Task<IEnumerable<NewsArticleCategory>> GetAllAsync()
        {
            return await newsArticleCategoryService.GetAllAsync();

            //var result = await newsArticleCategoryService.GetAllAsync();

            //if (result.Any())
            //    return new ApiResponse(ResponseStatusCode.Success, "Successfully returned Users list.");

            //return new ApiResponse(ResponseStatusCode.Error, "Could not return Users list.");
        }
    }
}