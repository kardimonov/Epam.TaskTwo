using TaskTwo.Data.Enums;
using TaskTwo.Data.Models;
using System.Threading.Tasks;

namespace TaskTwo.Logic.Interfaces
{
    public interface IMessageHandler
    {
        IMessageHandler Successor { get; set; }

        Task<MessageStatus> HandleRequest(Message message);
    }
}