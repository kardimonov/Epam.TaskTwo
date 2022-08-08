using FluentValidation;
using TaskTwo.Logic;
using TaskTwo.Web.ViewModels.SubjectVM;

namespace TaskTwo.Web.Validators
{
    public class SubjectCreateValidator : AbstractValidator<SubjectCreate>
    {
        public SubjectCreateValidator()
        {
            var settings = JsonAccessLayer.ReadDataFromJson();

            RuleFor(sc => sc.Name)
                .NotEmpty()
                .WithMessage($"Укажите название учебного предмета")
                .Length(settings.SubjectNameMinLength, settings.SubjectNameMaxLength)
                .WithMessage($"Поле должно иметь от {settings.SubjectNameMinLength} до {settings.SubjectNameMaxLength} символов.");
        }
    }
}