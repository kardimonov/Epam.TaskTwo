using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ITAcademy.TaskTwo.Data.Repositories
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(IApplicationContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Position>> GetAllPositionsWithEmployeesAsync() =>
            await Db.Positions
            .Include(p => p.Appointments)
            .ThenInclude(ep => ep.Employee)
            .ThenInclude(e => e.Phones)
            .Include(p => p.Appointments)
            .ThenInclude(ep => ep.Employee)
            .ThenInclude(e => e.Assignments)
            .ThenInclude(es => es.Subject)
            .OrderBy(s => s.Name)
            .ToListAsync();

        public async Task<Position> GetPositionWithEmployeesAsync(string name) =>
            await Db.Positions
            .Include(p => p.Appointments)
            .ThenInclude(ep => ep.Employee)
            .ThenInclude(e => e.Phones)
            .Include(p => p.Appointments)
            .ThenInclude(ep => ep.Employee)
            .ThenInclude(e => e.Assignments)
            .ThenInclude(es => es.Subject)
            .FirstOrDefaultAsync(s => s.Name == name);

        public async Task<Position> GetDetailsAsync(int id) => 
            await Db.Positions
            .Include(p => p.Appointments)
            .ThenInclude(ep => ep.Employee)
            .FirstOrDefaultAsync(p => p.Id == id);

        public override async Task<IEnumerable<Position>> SearchAsync(string searchString) =>
            await Db.Positions
            .Where(p => p.Name.Contains(searchString))
            .ToListAsync();

        public bool ExistsName(string name) =>
            Db.Positions
            .Any(s => s.Name == name);
    }
}