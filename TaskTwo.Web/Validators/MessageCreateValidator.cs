using FluentValidation;
using TaskTwo.Data.Enums;
using TaskTwo.Logic;
using TaskTwo.Web.ViewModels.MessageVM;

namespace TaskTwo.Web.Validators
{
    public class MessageCreateValidator : AbstractValidator<MessageCreate>
    {
        public MessageCreateValidator()
        {
            var settings = JsonAccessLayer.ReadDataFromJson();

            RuleFor(mc => mc.Content)
                .NotEmpty()
                .WithMessage($"Введите текст сообщения");

            RuleFor(mc => mc.Content)
                .MaximumLength(settings.SmsMessageContent).When(mc => mc.Type == MessageType.Sms)
                .WithMessage($"Сообщение не должно содержать более {settings.SmsMessageContent} символов")
                .MaximumLength(settings.EmailMessageContent).When(mc => mc.Type == MessageType.Email)
                .WithMessage($"Сообщение не должно содержать более {settings.EmailMessageContent} символов");
        }
    }
}