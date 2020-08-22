using AutoMapper;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Web.ViewModels.PhoneVM;

namespace ITAcademy.TaskTwo.Web.Profiles
{
    public class PhoneProfile : Profile
    {
        public PhoneProfile()
        {
            CreateMap<Employee, PhoneCreate>()
                .ForMember(pc => pc.EmployeeId, opt => opt.MapFrom(em => em.Id))
                .ForMember(pc => pc.EmployeeSurName, opt => opt.MapFrom(em => em.SurName))
                .ForMember(pc => pc.EmployeeFirstName, opt => opt.MapFrom(em => em.FirstName))
                .ForMember(pc => pc.EmployeeSecondName, opt => opt.MapFrom(em => em.SecondName))
                .ForMember(pc => pc.Method, opt => opt.MapFrom(
                    (src, dest, _, context) => context.Options.Items["Method"]));

            CreateMap<PhoneCreate, Phone>();

            CreateMap<Employee, PhoneIndex>();

            CreateMap<Phone, PhoneDelete>()
                .ForMember(pd => pd.Method, opt => opt.MapFrom(
                    (src, dest, _, context) => context.Options.Items["Method"]));

            CreateMap<Phone, PhoneEdit>()
                .ForMember(pc => pc.Method, opt => opt.MapFrom(
                    (src, dest, _, context) => context.Options.Items["Method"]));

            CreateMap<PhoneEdit, Phone>();
        }
    }}