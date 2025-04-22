using LerkaPr.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace LerkaPr
{
    public class ProjectDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LerkaDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public DbSet<StudentData> Students { get; set; }
        public DbSet<EmployeeData> Employees { get; set; }
        public DbSet<RoomData> Rooms { get; set; }
        public DbSet<ServiceData> Services { get; set; }
        public ProjectDbContext() { }
        public ProjectDbContext(DbContextOptions<ProjectDbContext> option) : base(option) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(CONNECTION_STRING);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
