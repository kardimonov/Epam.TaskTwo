using ITAcademy.TaskTwo.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Data.Interfaces
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<Subject> GetDetailsAsync(int id);

        bool ExistsName(string name);

        Task<IEnumerable<Subject>> GetAllSubjectsWithEmployeesAsync();

        Task<Subject> GetSubjectWithEmployeesAsync(string name);
    }
}