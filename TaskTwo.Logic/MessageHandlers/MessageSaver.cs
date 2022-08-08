using TaskTwo.Data.Enums;
using TaskTwo.Data.Interfaces;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Interfaces;
using System.Threading.Tasks;

namespace TaskTwo.Logic.MessageHandlers
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