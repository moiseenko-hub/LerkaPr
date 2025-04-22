using LerkaPr.Models.Database;
using LerkaPr.Models;
using LerkaPr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LerkaPr.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ServiceRepository _serviceRepository;

        public ServiceController(ServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public IActionResult Index()
        {
            var services = _serviceRepository.GetAll();
            var viewModels = services.Select(s => new ServiceViewModel
            {
                Id = s.Id,
                Type = s.Type,
                Description = s.Description,
                DateTime = s.DateTime
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ServiceViewModel service)
        {
            if (ModelState.IsValid)
            {
                var data = new ServiceData
                {
                    Type = service.Type,
                    Description = service.Description,
                    DateTime = service.DateTime
                };

                _serviceRepository.Add(data);
                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var service = _serviceRepository.Get(id);
            if (service == null) return NotFound();

            var viewModel = new ServiceViewModel
            {
                Id = service.Id,
                Type = service.Type,
                Description = service.Description,
                DateTime = service.DateTime
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, ServiceViewModel service)
        {
            if (id != service.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var data = new ServiceData
                {
                    Id = service.Id,
                    Type = service.Type,
                    Description = service.Description,
                    DateTime = service.DateTime
                };

                _serviceRepository.Update(data);
                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _serviceRepository.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
