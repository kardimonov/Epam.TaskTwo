using ITAcademy.TaskTwo.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Data.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllItemsWithAllListsAsync();

        Task<Employee> GetItemWithAllListsAsync(int id);

        Task<IEnumerable<Employee>> GetEmployeesWithPhonesAsync();

        Task<Employee> GetByPrimaryPhoneIdAsync(int phoneId);
    }
}