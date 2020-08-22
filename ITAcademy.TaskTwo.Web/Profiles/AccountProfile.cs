using AutoMapper;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Web.ViewModels.AccountVM;

namespace ITAcademy.TaskTwo.Web.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountRegister, User>();
        }
    }
}