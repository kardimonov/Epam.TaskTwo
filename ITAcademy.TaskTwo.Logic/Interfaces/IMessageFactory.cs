using ITAcademy.TaskTwo.Data.Enums;

namespace ITAcademy.TaskTwo.Logic.Interfaces
{
    public interface IMessageFactory
    {
        IMessageHandler GetMessageSender(MessageType messageType);
    }
}