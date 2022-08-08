using AutoMapper;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Models.EmployeeDTO;
using TaskTwo.Logic.Models.PositionDTO;
using TaskTwo.Logic.Models.SubjectDTO;
using System.Linq;

namespace TaskTwo.Logic.Profiles
{
    public class EmployeeDtoProfile : Profile
    {
        public EmployeeDtoProfile()
        {
            CreateMap<Employee, EmployeeWithAllLists>()
                .ForMember(ei => ei.Phones, opt => opt.MapFrom(e => e.Phones.Select(p => p.Number).ToList()))
                .ForMember(ewal => ewal.Assignments, opt => opt.MapFrom(
                    e => e.Assignments.Select(es => es.Subject.Name).ToList()))
                .ForMember(ewal => ewal.Appointments, opt => opt.MapFrom(
                    e => e.Appointments.Select(ep => ep.Position.Name).ToList()));

            CreateMap<Employee, EmployeeWithPhones>()
                .ForMember(ei => ei.Phones, opt => opt.MapFrom(e => e.Phones.Select(p => p.Number).ToList()));

            CreateMap<Employee, AssignedEmployeeDto>()
                .ForMember(aed => aed.Phones, opt => opt.MapFrom(e => e.Phones.Select(p => p.Number).ToList()));

            CreateMap<Employee, AppointedEmployeeDto>()
                .ForMember(aed => aed.Phones, opt => opt.MapFrom(e => e.Phones.Select(p => p.Number).ToList()))
                .ForMember(aed => aed.Subjects, opt => opt.MapFrom(
                    e => e.Assignments.Select(es => es.Subject.Name).ToList()));
        }
    }
}