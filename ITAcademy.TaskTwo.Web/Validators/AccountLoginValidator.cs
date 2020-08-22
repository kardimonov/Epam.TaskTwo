using FluentValidation;
using ITAcademy.TaskTwo.Web.ViewModels.AccountVM;

namespace ITAcademy.TaskTwo.Web.Validators
{
    public class AccountLoginValidator : AbstractValidator<AccountLogin>
    {
        public AccountLoginValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(ar => ar.Name)
                .NotEmpty()
                .WithMessage($"Укажите Login");

            RuleFor(ar => ar.Password)
                .NotEmpty()
                .WithMessage($"Укажите пароль");
        }
    }
}