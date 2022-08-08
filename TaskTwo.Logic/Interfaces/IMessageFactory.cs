using TaskTwo.Data.Enums;

namespace TaskTwo.Logic.Interfaces
{
    public interface IMessageFactory
    {
        IMessageHandler GetMessageSender(MessageType messageType);
    }
}