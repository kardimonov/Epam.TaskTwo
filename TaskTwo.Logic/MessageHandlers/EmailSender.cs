using TaskTwo.Data.Enums;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace TaskTwo.Logic.MessageHandlers
{
    public class EmailSender : IMessageHandler
    {
        public IMessageHandler Successor { get; set; }

        public async Task<MessageStatus> HandleRequest(Message message)
        {
            if (string.IsNullOrWhiteSpace(message.Addressee.Email))
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
                    await SendMessage(message);
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

        private async Task SendMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация SchoolManager", "school-manager-adm@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", message.Addressee.Email));
            emailMessage.Subject = "письмо отправленное через SchoolManager";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync("school-manager-adm@yandex.ru", "admin123");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}