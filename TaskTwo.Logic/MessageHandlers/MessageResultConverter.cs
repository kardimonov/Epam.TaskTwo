using TaskTwo.Data.Enums;
using System;

namespace TaskTwo.Logic.MessageHandlers
{
    public static class MessageResultConverter
    {
        public static string ConvertMessageStatusToString(MessageStatus input, string method)
        {
            string output;
            switch (input)
            {
                case MessageStatus.Success:
                    output = method == "SendAgain" ? "&#9989;" :
                        "The message has been successfully sent to the addressee and saved in the system.";
                    break;

                case MessageStatus.Failure:
                    output = method == "SendAgain" ? "&#10060;" :
                        "Message dispatch was cancelled with random token, but message was saved in the system.";
                    break;

                case MessageStatus.AddressNotFound:
                    output = method == "SendAgain" ? "&#9888;" :
                        "It is impossible to send the message, because the address is not found.";
                    break;

                case MessageStatus.UnexpectedError:
                    output = method == "SendAgain" ? "&#9888;" :
                        "Something went wrong when sending the message.";
                    break;

                default:
                    throw new NotSupportedException($"Message status {input} is not supported");
            }
            return output;
        }
    }
}