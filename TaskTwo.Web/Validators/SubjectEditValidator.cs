using FluentValidation;
using TaskTwo.Logic;
using TaskTwo.Web.ViewModels.SubjectVM;

namespace TaskTwo.Web.Validators
{
    public class SubjectEditValidator : AbstractValidator<SubjectEdit>
    {
        public SubjectEditValidator()
        {
            var settings = JsonAccessLayer.ReadDataFromJson();

            RuleFor(se => se.Name)
                .NotEmpty()
                .WithMessage($"Укажите название учебного предмета")
                .Length(settings.SubjectNameMinLength, settings.SubjectNameMaxLength)
                .WithMessage($"Поле должно иметь от {settings.SubjectNameMinLength} до {settings.SubjectNameMaxLength} символов.");
        }
    }
}