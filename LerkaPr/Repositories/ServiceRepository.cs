using LerkaPr.Models.Database;

namespace LerkaPr.Repositories
{
    public class ServiceRepository : BaselRepository<ServiceData>
    {
        public ServiceRepository(ProjectDbContext dbContext) : base(dbContext)
        {
        }
    }
}
