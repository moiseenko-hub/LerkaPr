using LerkaPr.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace LerkaPr.Repositories
{
    public class RoomRepository : BaselRepository<RoomData>
    {
        public RoomRepository(ProjectDbContext dbContext) : base(dbContext)
        {
        }

        public RoomData GetFromNumber(int number)
        {
            return _dbSet
                .Where(x => x.Number == number)
                .FirstOrDefault()!;
        }

        public override RoomData Get(int id)
        {
            return _dbSet
                .AsNoTracking()
                .Include(r => r.Students)
                .First(x => x.Id == id);
        }

        public override List<RoomData> GetAll()
        {
            return _dbSet
                .Include(r => r.Students)
                .ToList();
        }
    }
}
