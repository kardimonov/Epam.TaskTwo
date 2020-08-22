using FluentValidation;
using ITAcademy.TaskTwo.Logic;
using ITAcademy.TaskTwo.Web.ViewModels.SubjectVM;

namespace ITAcademy.TaskTwo.Web.Validators
{
    public class SubjectEditValidator : AbstractValidator<SubjectEdit>
    {
        public SubjectEditValidator()
        {
            var settings = JsonAccessLayer.ReadDataFromJson();
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(se => se.Name)
                .NotEmpty()
                .WithMessage($"Укажите название учебного предмета")
                .Length(settings.SubjectNameMinLength, settings.SubjectNameMaxLength)
                .WithMessage($"Поле должно иметь от {settings.SubjectNameMinLength} до {settings.SubjectNameMaxLength} символов.");
        }
    }
}