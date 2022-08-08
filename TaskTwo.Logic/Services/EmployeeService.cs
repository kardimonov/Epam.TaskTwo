using AutoMapper;
using TaskTwo.Data.Interfaces;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Interfaces;
using TaskTwo.Logic.Models.EmployeeDTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTwo.Logic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unit;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper map)
        {
            unit = unitOfWork;
            mapper = map;
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await unit.EmployeeRepo.GetAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithPhonesAsync()
        {
            return await unit.EmployeeRepo.GetEmployeesWithPhonesAsync();
        }

        public async Task<IEnumerable<EmployeeWithAllLists>> GetAllItemsWithAllListsAsync()
        {
            var employees = await unit.EmployeeRepo.GetAllItemsWithAllListsAsync();
            var employeesDto = new List<EmployeeWithAllLists>();
            foreach(var employee in employees)
            {
                employeesDto.Add(mapper.Map<EmployeeWithAllLists>(employee));
            }
            return employeesDto;
        }

        public async Task<EmployeeWithAllLists> GetItemWithAllListsAsync(int id)
        {
            var employee = await unit.EmployeeRepo.GetItemWithAllListsAsync(id);
            return mapper.Map<EmployeeWithAllLists>(employee);            
        }

        public async Task SetPrimaryPhoneAsync(Phone phone)
        {
            var employee = await unit.EmployeeRepo.GetAsync(phone.EmployeeId);
            if (employee.PrimaryPhoneId == null)
            {
                employee.PrimaryPhoneId = unit.PhoneRepo.GetPhoneIdByNumber(phone.Number);
            }
            else if (employee.PrimaryPhoneId == phone.Id)
            {
                employee.PrimaryPhoneId = employee.Phones.Count == 1 ? null :
                    (int?)employee.Phones.Where(p => p.Id != phone.Id).FirstOrDefault().Id;
            }
            unit.EmployeeRepo.Update(employee);
            unit.Save();
        }

        public async Task ResetPrimaryPhoneAsync(int phoneId)
        {
            var employee = await unit.EmployeeRepo.GetByPrimaryPhoneIdAsync(phoneId);
            employee.PrimaryPhoneId = phoneId;
            unit.EmployeeRepo.Update(employee);
            unit.Save();
        }
    }
}