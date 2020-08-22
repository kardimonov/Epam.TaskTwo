using System.Linq;
using AutoMapper;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Web.ViewModels.PositionVM;

namespace ITAcademy.TaskTwo.Web.Profiles
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<Position, PositionCreate>().ReverseMap();
            CreateMap<Position, PositionDelete>().ReverseMap();
            CreateMap<Position, PositionEdit>().ReverseMap();
            CreateMap<Position, PositionIndex>().ReverseMap();

            CreateMap<Position, PositionDetails>()
                .ForMember(pd => pd.Employees, opt =>
                opt.MapFrom(p => p.Appointments.Select(ep => ep.Employee).ToList()));
        }
    }
}