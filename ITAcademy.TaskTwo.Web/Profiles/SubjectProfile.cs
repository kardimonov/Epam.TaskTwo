using System.Linq;
using AutoMapper;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Web.ViewModels.SubjectVM;

namespace ITAcademy.TaskTwo.Web.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Subject, SubjectCreate>().ReverseMap();
            CreateMap<Subject, SubjectDelete>().ReverseMap();
            CreateMap<Subject, SubjectEdit>().ReverseMap();
            CreateMap<Subject, SubjectIndex>().ReverseMap();

            CreateMap<Subject, SubjectDetails>()
                .ForMember(sd => sd.Employees, opt =>
                opt.MapFrom(s => s.Assignments.Select(es => es.Employee).ToList()));
        }
    }
}