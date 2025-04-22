using LerkaPr.Models;
using LerkaPr.Models.Database;
using LerkaPr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LerkaPr.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Отобразить список сотрудников
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();
            var viewModels = employees
                .Select(e => new EmployeeViewModel()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    JobTitle = e.JobTitle
                }).ToList();
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            if (employee == null)
            {
                return BadRequest("Invalid employee data.");
            }

            if (ModelState.IsValid)
            {
                var employeeData = new EmployeeData()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    JobTitle = employee.JobTitle
                };

                _employeeRepository.Add(employeeData);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee == null)
            {
                return NotFound();
            }

            var viewModel = new EmployeeViewModel()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                JobTitle = employee.JobTitle
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, EmployeeViewModel employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var employeeData = new EmployeeData()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    JobTitle = employee.JobTitle
                };

                _employeeRepository.Update(employeeData);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _employeeRepository.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
