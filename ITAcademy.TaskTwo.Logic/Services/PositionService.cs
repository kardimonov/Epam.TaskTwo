using AutoMapper;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Interfaces;
using ITAcademy.TaskTwo.Logic.Models.PositionDTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Logic.Services
{
    public class PositionService : IPositionService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unit;

        public PositionService(IUnitOfWork unitOfWork, IMapper map)
        {
            unit = unitOfWork;
            mapper = map;
        }

        public async Task<Position> GetDetailsAsync(int id)
        {
            return await unit.PositionRepo.GetDetailsAsync(id);
        }

        public bool ExistsName(string name)
        {
            return unit.PositionRepo.ExistsName(name);
        }

        public async Task<PositionWithEmployees> GetEmployeesOfPositionAsync(Position position)
        {            
            var allEmployees = await unit.EmployeeRepo.GetAllAsync();
            var positionEmployees = new HashSet<int>(position.Appointments.Select(es => es.EmployeeId));
            var viewModel = new List<AppointedEmployee>();

            foreach (var employee in allEmployees)
            {
                viewModel.Add(mapper.Map<AppointedEmployee>(
                    employee, opt => opt.Items["Appointed"] = positionEmployees.Contains(employee.Id)));
            }
            return mapper.Map<PositionWithEmployees>(position, opt => opt.Items["AllEmployees"] = viewModel);
        }

        public async Task UpdateEmployeesOfPositionAsync(PositionWithEmployees model)
        {
            var positionToUpdate = await GetDetailsAsync(model.Id);
            foreach (var employee in model.AllEmployees)
            {
                if (employee.Appointed && (positionToUpdate.Appointments.Count == 0 ||
                        !positionToUpdate.Appointments.Any(ep => ep.EmployeeId == employee.Id)))
                {
                    positionToUpdate.Appointments.Add(
                        new EmployeePosition { PositionId = positionToUpdate.Id, EmployeeId = employee.Id });
                }
                else if (!employee.Appointed &&
                    positionToUpdate.Appointments.Any(ep => ep.EmployeeId == employee.Id))
                {
                    var employeeToRemove = positionToUpdate.Appointments.FirstOrDefault(
                        es => es.EmployeeId == employee.Id);
                    positionToUpdate.Appointments.Remove(employeeToRemove);
                }
            }
            unit.PositionRepo.Update(positionToUpdate);
            unit.Save();
        }

        public async Task<PositionWithEmployeesDto> GetPositionWithEmployeesAsync(string name)
        {
            var position = await unit.PositionRepo.GetPositionWithEmployeesAsync(name);
            return ConvertPositionToPositionWithEmployeesDto(position);
        }

        public async Task<IEnumerable<PositionWithEmployeesDto>> GetAllPositionsWithEmployeesAsync()
        {
            var positions = await unit.PositionRepo.GetAllPositionsWithEmployeesAsync();
            var positionsDto = new List<PositionWithEmployeesDto>();
            foreach (var position in positions)
            {
                positionsDto.Add(ConvertPositionToPositionWithEmployeesDto(position));
            }
            return positionsDto;
        }

        private PositionWithEmployeesDto ConvertPositionToPositionWithEmployeesDto(Position position)
        {
            var employeesDto = new List<AppointedEmployeeDto>();
            foreach (var employeePosition in position.Appointments)
            {
                employeesDto.Add(mapper.Map<AppointedEmployeeDto>(employeePosition.Employee));
            }
            return mapper.Map<PositionWithEmployeesDto>(
                position, opt => opt.Items["Employees"] = employeesDto);
        }
    }
}