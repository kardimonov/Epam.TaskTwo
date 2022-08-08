using FluentValidation;
using TaskTwo.Logic;
using TaskTwo.Web.ViewModels.EmployeeVM;

namespace TaskTwo.Web.Validators
{
    public class EmployeeEditValidator : AbstractValidator<EmployeeEdit>
    {
        public EmployeeEditValidator()
        {
            var settings = JsonAccessLayer.ReadDataFromJson();

            RuleFor(ec => ec.FirstName)
                .NotEmpty()
                .WithMessage($"Укажите имя")
                .Length(settings.NameMinLength, settings.NameMaxLength)
                .WithMessage($"Поле должно иметь от {settings.NameMinLength} до {settings.NameMaxLength} символов.");

            RuleFor(ec => ec.SecondName)
                .Length(settings.NameMinLength, settings.NameMaxLength)
                .WithMessage($"Поле должно иметь от {settings.NameMinLength} до {settings.NameMaxLength} символов или быть пустым.");

            RuleFor(ec => ec.SurName)
                .NotEmpty()
                .WithMessage($"Укажите фамилию")
                .Length(settings.NameMinLength, settings.NameMaxLength)
                .WithMessage($"Поле должно иметь от {settings.NameMinLength} до {settings.NameMaxLength} символов.");

            RuleFor(ec => ec.Birth)
                .NotEmpty()
                .WithMessage($"Укажите дату рождения");

            RuleFor(ec => ec.Email)
                .Length(0, settings.EmailMaxLength)
                .WithMessage($"Поле должно иметь не более {settings.EmailMaxLength} символов или быть пустым.")
                .EmailAddress()
                .WithMessage("Некорректный электронный адрес");
        }
    }
}