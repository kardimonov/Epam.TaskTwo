using TaskTwo.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTwo.Data.Interfaces
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<Position> GetDetailsAsync(int id);

        bool ExistsName(string name);

        Task<IEnumerable<Position>> GetAllPositionsWithEmployeesAsync();

        Task<Position> GetPositionWithEmployeesAsync(string name);
    }
}