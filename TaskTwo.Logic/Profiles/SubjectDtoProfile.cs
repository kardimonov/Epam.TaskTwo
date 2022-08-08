using AutoMapper;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Models.SubjectDTO;

namespace TaskTwo.Logic.Profiles
{
    public class SubjectDtoProfile : Profile
    {
        public SubjectDtoProfile()
        {
            CreateMap<Subject, SubjectDto>();

            CreateMap<Subject, SubjectWithEmployees>()
                .ForMember(see => see.AllEmployees, opt => opt.MapFrom(
                    (src, dest, _, context) => context.Options.Items["AllEmployees"]));

            CreateMap<Employee, AssignedEmployee>()
                .ForMember(ae => ae.Assigned, opt => opt.MapFrom(
                    (src, dest, _, context) => context.Options.Items["Assigned"]));

            CreateMap<Subject, SubjectWithEmployeesDto>()
                .ForMember(swed => swed.Employees, opt => opt.MapFrom(
                    (src, dest, _, context) => context.Options.Items["Employees"]));
        }
    }
}