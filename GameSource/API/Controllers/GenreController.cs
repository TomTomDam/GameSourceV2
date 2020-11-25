using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSource.Models;
using GameSource.Models.Enums;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.API.Controllers
{
    [Route("api/genre")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public IEnumerable<Genre> GetAll()
        {
            return genreService.GetAll();
        }

        [HttpGet]
        public async Task<ApiResponse> GetAllAsync()
        {
            var result = await genreService.GetAllAsync();

            if (result.Any())
                return new ApiResponse(ResponseStatusCode.Success, "Successfully returned Users list.");

            return new ApiResponse(ResponseStatusCode.Error, "Could not return Users list.");
        }
    }
}