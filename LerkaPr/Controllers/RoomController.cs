using LerkaPr.Models;
using LerkaPr.Models.Database;
using LerkaPr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LerkaPr.Controllers
{
    public class RoomController : Controller
    {
        private readonly RoomRepository _roomRepository;
        private readonly StudentRepository _studentRepository;

        public RoomController(RoomRepository roomRepository, StudentRepository studentRepository)
        {
            _roomRepository = roomRepository;
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            var rooms = _roomRepository.GetAll();
            var viewModels = rooms
                .Select(r => new RoomViewModel
                {
                    Id = r.Id,
                    Number = r.Number
                }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var room = _roomRepository.Get(id);
            if (room == null)
                return NotFound();

            var students = room.Students.Select(s => new StudentViewModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                LastVisit = s.LastVisit,
                RoomNumber = room.Number
            }).ToList();

            var viewModel = new RoomViewModel
            {
                Id = room.Id,
                Number = room.Number,
                Students = students
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomViewModel room)
        {
            if (room == null)
                return BadRequest("Invalid room data.");

            if (ModelState.IsValid)
            {
                var roomData = new RoomData
                {
                    Number = room.Number
                };

                _roomRepository.Add(roomData);
                return RedirectToAction(nameof(Index));
            }

            return View(room);
        }

        public IActionResult Edit(int id)
        {
            var room = _roomRepository.Get(id);
            if (room == null)
                return NotFound();

            var allStudents = _studentRepository.GetAll();

            var viewModel = new RoomViewModel
            {
                Id = room.Id,
                Number = room.Number,
                SelectedStudentIds = room.Students.Select(s => s.Id).ToList(),
                Students = allStudents.Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    RoomNumber = _roomRepository.Get(s.RoomId).Number
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int id, RoomViewModel room)
        {
            if (id != room.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var roomData = _roomRepository.Get(id);
                if (roomData == null) return NotFound();

                roomData.Number = room.Number;

                var allStudents = _studentRepository.GetAll();
                foreach (var student in allStudents)
                {
                    if (room.SelectedStudentIds.Contains(student.Id))
                    {
                        student.RoomId = room.Id;
                    }
                    else if (student.RoomId == room.Id)
                    {
                        student.RoomId = 0;
                    }
                }

                _roomRepository.Update(roomData);
                return RedirectToAction(nameof(Index));
            }

            room.Students = _studentRepository.GetAll()
                .Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    RoomNumber = _roomRepository.Get(s.RoomId).Number
                }).ToList();

            return View(room);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _roomRepository.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
