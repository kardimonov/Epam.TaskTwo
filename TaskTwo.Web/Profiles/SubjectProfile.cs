using System.Linq;
using AutoMapper;
using TaskTwo.Data.Models;
using TaskTwo.Web.ViewModels.SubjectVM;

namespace TaskTwo.Web.Profiles
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