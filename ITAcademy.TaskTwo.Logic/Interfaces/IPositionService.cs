using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Models.PositionDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Logic.Interfaces
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