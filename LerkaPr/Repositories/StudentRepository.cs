using LerkaPr.Models.Database;

namespace LerkaPr.Repositories
{
    public class StudentRepository : BaselRepository<StudentData>
    {
        public StudentRepository(ProjectDbContext dbContext) : base(dbContext){}
    }
}
