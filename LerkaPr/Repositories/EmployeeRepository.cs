using LerkaPr.Models.Database;

namespace LerkaPr.Repositories
{
    public class EmployeeRepository : BaselRepository<EmployeeData>
    {
        public EmployeeRepository(ProjectDbContext dbContext) : base(dbContext)
        {
        }
    }
}
