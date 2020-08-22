using FluentValidation;
using ITAcademy.TaskTwo.Logic;
using ITAcademy.TaskTwo.Web.ViewModels.SubjectVM;

namespace ITAcademy.TaskTwo.Web.Validators
{
    public class SubjectCreateValidator : AbstractValidator<SubjectCreate>
    {
        public SubjectCreateValidator()
        {
            var settings = JsonAccessLayer.ReadDataFromJson();
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(sc => sc.Name)
                .NotEmpty()
                .WithMessage($"Укажите название учебного предмета")
                .Length(settings.SubjectNameMinLength, settings.SubjectNameMaxLength)
                .WithMessage($"Поле должно иметь от {settings.SubjectNameMinLength} до {settings.SubjectNameMaxLength} символов.");
        }
    }
}