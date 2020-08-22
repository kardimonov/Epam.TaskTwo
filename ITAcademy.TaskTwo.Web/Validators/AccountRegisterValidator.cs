using FluentValidation;
using ITAcademy.TaskTwo.Logic;
using ITAcademy.TaskTwo.Web.ViewModels.AccountVM;

namespace ITAcademy.TaskTwo.Web.Validators
{
    public class AccountRegisterValidator : AbstractValidator<AccountRegister>
    {
        public AccountRegisterValidator()
        {
            var settings = JsonAccessLayer.ReadDataFromJson();
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(ar => ar.Name)
                .NotEmpty()
                .WithMessage($"Укажите Login")
                .Length(settings.UserNameMinLength, settings.UserNameMaxLength)
                .WithMessage($"Поле должно иметь от {settings.UserNameMinLength} до {settings.UserNameMaxLength} символов.");

            RuleFor(ar => ar.Email)
                .NotEmpty()
                .WithMessage($"Укажите электронный адрес")
                .Length(0, settings.EmailMaxLength)
                .WithMessage($"Поле должно иметь не более {settings.EmailMaxLength} символов.")
                .EmailAddress()
                .WithMessage("Некорректный электронный адрес");

            RuleFor(ar => ar.Password)
                .NotEmpty()
                .WithMessage($"Укажите пароль")
                .Length(settings.PasswordMinLength, settings.PasswordMaxLength)
                .WithMessage($"Поле должно иметь от {settings.PasswordMinLength} до {settings.PasswordMaxLength} символов.");

            RuleFor(ar => ar.PasswordConfirm)
                .NotEmpty()
                .WithMessage($"Введите повторно пароль");
        }
    }
}