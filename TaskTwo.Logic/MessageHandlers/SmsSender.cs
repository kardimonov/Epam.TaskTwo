using TaskTwo.Data.Enums;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Interfaces;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTwo.Logic.MessageHandlers
{
    public class SmsSender : IMessageHandler
    {
        public IMessageHandler Successor { get; set; }
        
        public async Task<MessageStatus> HandleRequest(Message message)
        {
            if (message.Addressee.Phones.Count == 0)
            {
                return MessageStatus.AddressNotFound;
            }
            else if (Successor != null)
            {                
                try
                {
                    var rand = new Random(message.Content.Length);
                    if (rand.NextDouble() < 1.0 / 3)
                    {
                        throw new Exception();
                    }
                    SendMessage(message);
                    message.DispatchResult = MessageStatus.Success;
                }
                catch
                {
                }
                finally
                {
                    await Successor.HandleRequest(message);
                }
                return message.DispatchResult;
            }
            return MessageStatus.UnexpectedError;
        }
        
        private static void SendMessage(Message message)
        {
            var phoneNumber = message.Addressee.Phones
                .FirstOrDefault(p => p.Id == message.Addressee.PrimaryPhoneId).Number;
            Log.Information(
                $"Sending {message.Type} to employee: {message.Addressee.SurName} {message.Addressee.FirstName}" +
                $" {message.Addressee.SecondName}, address: {phoneNumber}, Text: {message.Content}");
        }        
    }
}