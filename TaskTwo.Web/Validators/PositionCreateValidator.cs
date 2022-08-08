using FluentValidation;
using TaskTwo.Logic;
using TaskTwo.Web.ViewModels.PositionVM;

namespace TaskTwo.Web.Validators
{
    public class PositionCreateValidator : AbstractValidator<PositionCreate>
    {
        public PositionCreateValidator()
        {
            var settings = JsonAccessLayer.ReadDataFromJson();

            RuleFor(pc => pc.Name)
                .NotEmpty()
                .WithMessage($"Укажите название должности")
                .Length(settings.PositionNameMinLength, settings.PositionNameMaxLength)
                .WithMessage($"Поле должно иметь от {settings.PositionNameMinLength} до {settings.PositionNameMaxLength} символов.");

            RuleFor(pc => pc.MaxNumber)
                .NotEmpty()
                .WithMessage($"Укажите максимальное количество штатных единиц");
        }
    }
}