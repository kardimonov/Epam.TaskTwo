using AutoMapper;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Interfaces;
using ITAcademy.TaskTwo.Logic.Models.SubjectDTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Logic.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unit;

        public SubjectService(IUnitOfWork unitOfWork, IMapper map)
        {
            unit = unitOfWork;
            mapper = map;
        }
                
        public async Task<Subject> GetDetailsAsync(int id)
        {
            return await unit.SubjectRepo.GetDetailsAsync(id);
        }

        public bool ExistsName(string name)
        {
            return unit.SubjectRepo.ExistsName(name);
        }

        public async Task<SubjectWithEmployees> GetEmployeesOfSubjectAsync(Subject subject)
        {            
            var allEmployees = await unit.EmployeeRepo.GetAllAsync();
            var subjectEmployees = new HashSet<int>(subject.Assignments.Select(es => es.EmployeeId));
            var viewModel = new List<AssignedEmployee>();

            foreach (var employee in allEmployees)
            {
                viewModel.Add(mapper.Map<AssignedEmployee>(
                    employee, opt => opt.Items["Assigned"] = subjectEmployees.Contains(employee.Id)));
            }
            return mapper.Map<SubjectWithEmployees>(subject, opt => opt.Items["AllEmployees"] = viewModel);
        }

        public async Task UpdateEmployeesOfSubject(SubjectWithEmployees model)
        {
            var subjectToUpdate = await GetDetailsAsync(model.Id);
            foreach (var employee in model.AllEmployees)
            {
                if (employee.Assigned && (subjectToUpdate.Assignments.Count == 0 ||
                    !subjectToUpdate.Assignments.Any(es => es.EmployeeId == employee.Id)))
                {
                    subjectToUpdate.Assignments.Add(
                        new EmployeeSubject { SubjectId = subjectToUpdate.Id, EmployeeId = employee.Id });
                }
                else if (!employee.Assigned &&
                    subjectToUpdate.Assignments.Any(es => es.EmployeeId == employee.Id))
                {
                    var employeeToRemove = subjectToUpdate.Assignments.FirstOrDefault(
                        es => es.EmployeeId == employee.Id);
                    subjectToUpdate.Assignments.Remove(employeeToRemove);
                }
            }
            unit.SubjectRepo.Update(subjectToUpdate);
            unit.Save();
        }

        public async Task<SubjectWithEmployeesDto> GetSubjectWithEmployeesAsync(string name)
        {
            var subject = await unit.SubjectRepo.GetSubjectWithEmployeesAsync(name);
            return ConvertSubjectToSubjectWithEmployeesDto(subject);
        }

        public async Task<IEnumerable<SubjectWithEmployeesDto>> GetAllSubjectsWithEmployeesAsync()
        {
            var subjects = await unit.SubjectRepo.GetAllSubjectsWithEmployeesAsync();
            var subjectsDto = new List<SubjectWithEmployeesDto>();
            foreach (var subject in subjects)
            {
                subjectsDto.Add(ConvertSubjectToSubjectWithEmployeesDto(subject));
            }
            return subjectsDto;
        }

        private SubjectWithEmployeesDto ConvertSubjectToSubjectWithEmployeesDto(Subject subject)
        {
            var employeesDto = new List<AssignedEmployeeDto>();
            foreach (var employeeSubject in subject.Assignments)
            {
                employeesDto.Add(mapper.Map<AssignedEmployeeDto>(employeeSubject.Employee));
            }
            return mapper.Map<SubjectWithEmployeesDto>(
                subject, opt => opt.Items["Employees"] = employeesDto);
        }
    }
}