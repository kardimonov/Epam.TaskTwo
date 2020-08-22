using FluentValidation;
using ITAcademy.TaskTwo.Logic;
using ITAcademy.TaskTwo.Web.ViewModels.PositionVM;

namespace ITAcademy.TaskTwo.Web.Validators
{
    public class PositionCreateValidator : AbstractValidator<PositionCreate>
    {
        public PositionCreateValidator()
        {
            var settings = JsonAccessLayer.ReadDataFromJson();
            CascadeMode = CascadeMode.StopOnFirstFailure;

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