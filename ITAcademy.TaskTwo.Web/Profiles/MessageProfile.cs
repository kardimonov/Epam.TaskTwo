using AutoMapper;
using ITAcademy.TaskTwo.Data.Enums;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic;
using ITAcademy.TaskTwo.Web.ViewModels.MessageVM;
using System;

namespace ITAcademy.TaskTwo.Web.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageIndex>()
                .ForMember(mi => mi.FullName, opt => opt.MapFrom(
                    m => m.Addressee.SurName + " " + m.Addressee.FirstName + " " + m.Addressee.SecondName));

            CreateMap<Employee, MessageCreate>()
                .ForMember(mc => mc.AddresseeId, opt => opt.MapFrom(e => e.Id))
                .ForMember(mc => mc.FullName, opt => opt.MapFrom(
                    e => e.SurName + " " + e.FirstName + " " + e.SecondName))
                .ForMember(mc => mc.Type, opt => opt.MapFrom(e => e.Communication))
                .ForMember(mc => mc.MaxLength, opt => opt.MapFrom(
                    e => e.Communication == MessageType.Email ?
                    JsonAccessLayer.ReadDataFromJson().EmailMessageContent :
                    JsonAccessLayer.ReadDataFromJson().SmsMessageContent));

            CreateMap<MessageCreate, Message>()
                .ForMember(m => m.TimeCreated, opt => opt.MapFrom(mc => DateTime.Now))
                .ForMember(m => m.DispatchResult, opt => opt.MapFrom(mc => MessageStatus.Failure))
                .ForMember(m => m.Addressee, opt => opt.MapFrom(
                    (src, dest, _, context) => context.Options.Items["Addressee"]));
        }
    }
}