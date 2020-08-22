using ITAcademy.TaskTwo.Data.Enums;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Interfaces;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Logic.MessageHandlers
{
    public class MessageSaver : IMessageHandler
    {
        private readonly IUnitOfWork unit;

        public MessageSaver(IUnitOfWork unitOfWork)
        {
            unit = unitOfWork;
        }

        public IMessageHandler Successor { get; set; }

        public async Task<MessageStatus> HandleRequest(Message message)
        {
            await unit.MessageRepo.CreateAsync(message);
            unit.Save();
            return message.DispatchResult;
        }
    }
}