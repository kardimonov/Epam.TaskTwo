using AutoMapper;
using TaskTwo.Data.Models;
using TaskTwo.Web.ViewModels.EmployeeVM;
using System.Linq;

namespace TaskTwo.Web.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeCreate>().ReverseMap();
            CreateMap<Employee, EmployeeDelete>().ReverseMap();
            CreateMap<Employee, EmployeeEdit>().ReverseMap();

             CreateMap<Employee, EmployeeIndex>()
                .ForMember(ei => ei.Phones, opt => opt.MapFrom(em => em.Phones.Select(p => p.Number).ToList()));            
        }
    }
}