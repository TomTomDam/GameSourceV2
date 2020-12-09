using System.Linq;
using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.PlatformViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameSource.Controllers.GameSource
{
    [Route("platform")]
    public class PlatformController : Controller
    {
        private IPlatformService platformService;
        private IPlatformTypeService platformTypeService;

        public PlatformController(IPlatformService platformService, IPlatformTypeService platformTypeService)
        {
            this.platformService = platformService;
            this.platformTypeService = platformTypeService;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            PlatformIndexViewModel viewModel = new PlatformIndexViewModel
            {
                Platforms = platformService.GetAll(),
                PlatformTypes = platformTypeService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            if (id == 0)
                return NotFound();

            Platform platform = platformService.GetByID(id);
            if (platform == null)
                return NotFound();

            PlatformType platformType = platformTypeService.GetByID((int)platform.PlatformTypeID);

            PlatformDetailsViewModel viewModel = new PlatformDetailsViewModel
            {
                Platform = platform,
                PlatformType = platformType ?? null
            };

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            PlatformCreateViewModel viewModel = new PlatformCreateViewModel();
            viewModel.Platform = new Platform();
            viewModel.PlatformTypes = platformTypeService.GetAll().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PlatformCreateViewModel viewModel)
        {
            Platform platform = new Platform
            {
                ID = viewModel.Platform.ID,
                Name = viewModel.Platform.Name,
                PlatformTypeID = viewModel.Platform.PlatformTypeID ?? null
            };

            platformService.Insert(platform);
            return RedirectToAction("Index");
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            if (id == 0)
                return NotFound();

            Platform platform = platformService.GetByID(id);
            if (platform == null)
                return NotFound();

            PlatformEditViewModel viewModel = new PlatformEditViewModel();
            viewModel.Platform = platform;
            viewModel.PlatformTypes = platformTypeService.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            viewModel.PlatformTypeID = platform.PlatformTypeID ?? null;

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PlatformEditViewModel viewModel)
        {
            Platform platform = platformService.GetByID(viewModel.Platform.ID);

            platform.Name = viewModel.Platform.Name;
            platform.PlatformTypeID = viewModel.Platform.PlatformTypeID ?? null;

            platformService.Update(platform);
            return RedirectToAction("Details", platform);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Platform platform = platformService.GetByID(id);
            if (platform == null)
                return NotFound();

            PlatformDeleteViewModel viewModel = new PlatformDeleteViewModel
            {
                Platform = platform,
                PlatformType = platformTypeService.GetByID((int)platform.PlatformTypeID) ?? null
            };

            return View(viewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PlatformDeleteViewModel viewModel)
        {
            Platform platform = platformService.GetByID(viewModel.Platform.ID);
            if (platform == null)
                return NotFound();

            platformService.Delete(platform.ID);
            return RedirectToAction("Index");
        }
    }
}