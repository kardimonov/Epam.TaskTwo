using ITAcademy.TaskTwo.Data.Enums;
using ITAcademy.TaskTwo.Data.Models;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Logic.Interfaces
{
    public interface IMessageHandler
    {
        IMessageHandler Successor { get; set; }

        Task<MessageStatus> HandleRequest(Message message);
    }
}