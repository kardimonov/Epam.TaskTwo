using TaskTwo.Data.Models;
using TaskTwo.Logic.Models.PositionDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTwo.Logic.Interfaces
{
    public interface IPositionService : IBaseService
    {
        Task<Position> GetDetailsAsync(int id);

        bool ExistsName(string name);

        Task UpdateEmployeesOfPositionAsync(PositionWithEmployees model);

        Task<PositionWithEmployees> GetEmployeesOfPositionAsync(Position position);

        Task<PositionWithEmployeesDto> GetPositionWithEmployeesAsync(string name);

        Task<IEnumerable<PositionWithEmployeesDto>> GetAllPositionsWithEmployeesAsync();
    }
}