using TaskTwo.Data.Models;
using TaskTwo.Logic.Models.SubjectDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTwo.Logic.Interfaces
{
    public interface ISubjectService : IBaseService
    {
        Task<Subject> GetDetailsAsync(int id);

        bool ExistsName(string name);

        Task UpdateEmployeesOfSubject(SubjectWithEmployees model);

        Task<SubjectWithEmployees> GetEmployeesOfSubjectAsync(Subject subject);

        Task<SubjectWithEmployeesDto> GetSubjectWithEmployeesAsync(string name);

        Task<IEnumerable<SubjectWithEmployeesDto>> GetAllSubjectsWithEmployeesAsync();
    }
}