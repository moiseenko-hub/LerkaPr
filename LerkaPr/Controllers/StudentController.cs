using LerkaPr.Models;
using LerkaPr.Models.Database;
using LerkaPr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LerkaPr.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentRepository _studentRepository;
        private readonly RoomRepository _roomRepository;

        public StudentController(StudentRepository studentRepository, RoomRepository roomRepository)
        {
            _studentRepository = studentRepository;
            _roomRepository = roomRepository;
        }

        public IActionResult Index()
        {
            var students = _studentRepository.GetAll();
            var viewModels = students
                .Select(s => new StudentViewModel()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    LastVisit = s.LastVisit,
                    RoomNumber = _roomRepository.Get(s.RoomId).Number
                }).ToList();
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentViewModel student)
        {
            if (student == null)
            {
                return BadRequest("Invalid student data.");
            }

            var room = _roomRepository.GetFromNumber(student.RoomNumber);
            if (room == null)
            {
                ModelState.AddModelError("RoomNumber", "The specified room does not exist.");
            }

            if (ModelState.IsValid)
            {
                var studentData = new StudentData()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    LastVisit = student.LastVisit,
                    RoomId = room.Id
                };

                _studentRepository.Add(studentData);
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }



        public IActionResult Edit(int id)
        {
            var student = _studentRepository.Get(id);
            var viewModel = new StudentViewModel()
            {
                Id = id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                LastVisit = student.LastVisit,
                RoomNumber = _roomRepository.Get(student.RoomId).Number
            };
            if (student == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, StudentViewModel student)
        {

            if (id != student.Id)
            {
                return BadRequest();
            }
            var room = _roomRepository.GetFromNumber(student.RoomNumber);
            var studentData = new StudentData()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                LastVisit = student.LastVisit,
                RoomId = room.Id
            };

            if (ModelState.IsValid)
            {
                _studentRepository.Update(studentData);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _studentRepository.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
