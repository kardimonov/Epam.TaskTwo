using FluentValidation;
using ITAcademy.TaskTwo.Data.Enums;
using ITAcademy.TaskTwo.Logic;
using ITAcademy.TaskTwo.Web.ViewModels.MessageVM;

namespace ITAcademy.TaskTwo.Web.Validators
{
    public class MessageCreateValidator : AbstractValidator<MessageCreate>
    {
        public MessageCreateValidator()
        {
            var settings = JsonAccessLayer.ReadDataFromJson();
            CascadeMode = CascadeMode.StopOnFirstFailure;

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