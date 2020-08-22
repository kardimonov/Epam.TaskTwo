using ITAcademy.TaskTwo.Data.Enums;
using ITAcademy.TaskTwo.Logic.Interfaces;
using System;

namespace ITAcademy.TaskTwo.Logic.MessageHandlers
{
    public class MessageFactory : IMessageFactory
    {
        public IMessageHandler GetMessageSender(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Email:
                    return new EmailSender();
                case MessageType.Sms:
                    return new SmsSender();
                default: 
                    throw new NotSupportedException($"Message type {messageType} is not supported");
            }
        }
    }
}