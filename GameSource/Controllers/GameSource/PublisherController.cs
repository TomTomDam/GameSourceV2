using GameSource.Models.GameSource;
using GameSource.Services.GameSource.Contracts;
using GameSource.ViewModels.GameSource.PublisherViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GameSource.Controllers.GameSource
{
    [Route("publisher")]
    public class PublisherController : Controller
    {
        private IPublisherService publisherService;
        public PublisherController(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        [HttpGet("index")]
        public IActionResult Index()
        {
            PublisherIndexViewModel viewModel = new PublisherIndexViewModel
            {
                Publishers = publisherService.GetAll()
            };

            return View(viewModel);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            if (id == 0)
                return NotFound();

            Publisher publisher = publisherService.GetByID(id);
            if (publisher == null)
                return NotFound();

            PublisherDetailsViewModel viewModel = new PublisherDetailsViewModel();
            viewModel.Publisher = publisher;

            return View(viewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            PublisherCreateViewModel viewModel = new PublisherCreateViewModel();
            viewModel.Publisher = new Publisher();

            return View(viewModel);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PublisherCreateViewModel viewModel)
        {
            Publisher publisher = new Publisher
            {
                ID = viewModel.Publisher.ID,
                Name = viewModel.Publisher.Name
            };

            publisherService.Insert(publisher);
            return RedirectToAction("Index");
        }


        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            if (id == 0)
                return NotFound();

            Publisher publisher = publisherService.GetByID(id);
            if (publisher == null)
                return NotFound();

            PublisherEditViewModel viewModel = new PublisherEditViewModel();
            viewModel.Publisher = publisher;

            return View(viewModel);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PublisherEditViewModel viewModel)
        {
            Publisher publisher = publisherService.GetByID(viewModel.Publisher.ID);

            publisher.Name = viewModel.Publisher.Name;

            publisherService.Update(publisher);
            return RedirectToAction("Details", publisher);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Publisher publisher = publisherService.GetByID(id);
            if (publisher == null)
                return NotFound();

            PublisherDeleteViewModel viewModel = new PublisherDeleteViewModel
            {
                Publisher = publisher
            };

            return View(viewModel);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(PublisherDeleteViewModel viewModel)
        {
            Publisher publisher = publisherService.GetByID(viewModel.Publisher.ID);
            if (publisher == null)
                return NotFound();

            publisherService.Delete(publisher.ID);
            return RedirectToAction("Index");
        }
    }
}