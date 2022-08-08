using AutoMapper;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Models.PositionDTO;

namespace TaskTwo.Logic.Profiles
{
    public class PositionDtoProfile : Profile
    {
        public PositionDtoProfile()
        {
            CreateMap<Position, PositionDto>();

            CreateMap<Position, PositionWithEmployees>()
                .ForMember(pwe => pwe.AllEmployees, opt => opt.MapFrom(
                    (src, dest, _, context) => context.Options.Items["AllEmployees"]));

            CreateMap<Employee, AppointedEmployee>()
                .ForMember(ae => ae.Appointed, opt => opt.MapFrom(
                    (src, dest, _, context) => context.Options.Items["Appointed"]));

            CreateMap<Position, PositionWithEmployeesDto>()
                .ForMember(pwed => pwed.Employees, opt => opt.MapFrom(
                    (src, dest, _, context) => context.Options.Items["Employees"]))
                .ForMember(pwed => pwed.NumberOfVacancies, opt => opt.MapFrom(
                    p => p.MaxNumber - p.Appointments.Count ));
        }
    }
}