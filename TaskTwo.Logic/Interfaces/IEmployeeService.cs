using TaskTwo.Data.Models;
using TaskTwo.Logic.Models.EmployeeDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTwo.Logic.Interfaces
{
    public interface IEmployeeService : IBaseService
    {
        Task<Employee> GetAsync(int id);

        Task<IEnumerable<EmployeeWithAllLists>> GetAllItemsWithAllListsAsync();

        Task<EmployeeWithAllLists> GetItemWithAllListsAsync(int id);

        Task<IEnumerable<Employee>> GetEmployeesWithPhonesAsync();

        Task SetPrimaryPhoneAsync(Phone phone);

        Task ResetPrimaryPhoneAsync(int id);
    }
}