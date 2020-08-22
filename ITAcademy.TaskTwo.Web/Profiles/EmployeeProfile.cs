using AutoMapper;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Web.ViewModels.EmployeeVM;
using System.Linq;

namespace ITAcademy.TaskTwo.Web.Profiles
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