using AutoMapper;
using TaskTwo.Data.Models;
using TaskTwo.Web.ViewModels.AccountVM;

namespace TaskTwo.Web.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountRegister, User>();
        }
    }
}