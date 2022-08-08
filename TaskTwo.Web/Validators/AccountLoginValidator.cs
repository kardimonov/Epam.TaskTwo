using FluentValidation;
using TaskTwo.Web.ViewModels.AccountVM;

namespace TaskTwo.Web.Validators
{
    public class AccountLoginValidator : AbstractValidator<AccountLogin>
    {
        public AccountLoginValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(ar => ar.Name)
                .NotEmpty()
                .WithMessage($"Укажите Login");

            RuleFor(ar => ar.Password)
                .NotEmpty()
                .WithMessage($"Укажите пароль");
        }
    }
}