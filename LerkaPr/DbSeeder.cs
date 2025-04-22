using LerkaPr.Models.Database;
using System;

namespace LerkaPr
{
    public static class DbSeeder
    {
        public static void Seed(ProjectDbContext context)
        {
            // Комнаты
            if (!context.Rooms.Any())
            {
                var rooms = new List<RoomData>
                {
                    new() { Number = 101 },
                    new() { Number = 102 },
                    new() { Number = 201 }
                };
                context.Rooms.AddRange(rooms);
                context.SaveChanges();
            }

            // Студенты
            if (!context.Students.Any())
            {
                var students = new List<StudentData>
                {
                    new()
                    {
                        FirstName = "Иван",
                        LastName = "Иванов",
                        LastVisit = DateTime.Now.AddDays(-2),
                        RoomId = context.Rooms.OrderBy(r => r.Id).First().Id
                    },
                    new()
                    {
                        FirstName = "Мария",
                        LastName = "Петрова",
                        LastVisit = DateTime.Now.AddDays(-1),
                        RoomId = context.Rooms.OrderBy(r => r.Id).Last().Id
                    }
                };
                context.Students.AddRange(students);
                context.SaveChanges();
            }

            // Услуги
            if (!context.Services.Any())
            {
                var services = new List<ServiceData>
                {
                    new() { Type = "Уборка", Description = "Уборка общежития после выходных", DateTime = DateTime.Now.AddDays(-3) },
                    new() { Type = "Проверка", Description = "Проверка электросети в корпусе A", DateTime = DateTime.Now.AddDays(-1) }
                };
                context.Services.AddRange(services);
                context.SaveChanges();
            }

            // Сотрудники
            if (!context.Employees.Any())
            {
                var employees = new List<EmployeeData>
                {
                    new() { FirstName = "Анна", LastName = "Кузнецова", JobTitle = "Комендант" },
                    new() { FirstName = "Олег", LastName = "Сидоров", JobTitle = "Техник" },
                    new() { FirstName = "Светлана", LastName = "Морозова", JobTitle = "Уборщица" }
                };
                context.Employees.AddRange(employees);
                context.SaveChanges();
            }
        }
    }
}
