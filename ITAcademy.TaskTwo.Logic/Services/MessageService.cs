using ITAcademy.TaskTwo.Data.Enums;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Interfaces;
using ITAcademy.TaskTwo.Logic.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Logic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unit;
        private readonly IMessageFactory factory;

        public MessageService(IUnitOfWork unitOfWork, IMessageFactory messageFactory)
        {
            unit = unitOfWork;
            factory = messageFactory;
        }

        public IMessageHandler Sender { get; set; }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await unit.MessageRepo.GetAllAsync();
        }

        public async Task<Message> GetAsync(int id)
        {
            return await unit.MessageRepo.GetAsync(id);
        }

        public async Task<Message> GetWithAddresseeAsync(int id)
        {
            var message = await unit.MessageRepo.GetNoTrackingAsync(id);
            message.Id = 0;
            message.TimeCreated = DateTime.Now;
            message.Addressee = await unit.EmployeeRepo.GetAsync(message.AddresseeId);
            message.DispatchResult = MessageStatus.Failure;
            return message;
        }

        public async Task<string> HandleMessageAsync(Message message, string method)
        {
            Sender = factory.GetMessageSender(message.Type);
            Sender.Successor = new MessageSaver(unit);
            var result = await Sender.HandleRequest(message);
            return MessageResultConverter.ConvertMessageStatusToString(result, method);
        }        
    }
}