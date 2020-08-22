using FluentValidation;
using ITAcademy.TaskTwo.Data.Context;
using ITAcademy.TaskTwo.Logic;
using ITAcademy.TaskTwo.Web.ViewModels.PhoneVM;
using System.Linq;

namespace ITAcademy.TaskTwo.Web.Validators
{
    public class PhoneEditValidator : AbstractValidator<PhoneEdit>
    {
        private readonly ApplicationContext db;

        public PhoneEditValidator(ApplicationContext context)
        {
            db = context;
            var settings = JsonAccessLayer.ReadDataFromJson();
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(pc => pc.Number)
                .NotEmpty()
                .WithMessage($"Укажите номер телефона")
                .Matches(settings.PhonePattern)
                .WithMessage($"Некорректный номер телефона")
                .Must((source, phoneNumber) => VerifyPhoneNumber(source.EmployeeId, source.Id, phoneNumber))
                .WithMessage($"Такой номер телефона уже есть в базе у данного сотрудника");
        }

        private bool VerifyPhoneNumber(int employeeId, int id, string number) =>
            !db.Phones.Any(p => p.Number == number && p.EmployeeId == employeeId) || 
                db.Phones.Any(p => p.Number == number && p.EmployeeId == employeeId && p.Id == id);
    }
}