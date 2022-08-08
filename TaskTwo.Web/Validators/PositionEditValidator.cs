using System.Linq;
using FluentValidation;
using TaskTwo.Data.Context;
using TaskTwo.Logic;
using TaskTwo.Web.ViewModels.PositionVM;
using Microsoft.EntityFrameworkCore;

namespace TaskTwo.Web.Validators
{
    public class PositionEditValidator : AbstractValidator<PositionEdit>
    {
        private readonly ApplicationContext db;

        public PositionEditValidator(ApplicationContext context)
        {
            db = context;
            var settings = JsonAccessLayer.ReadDataFromJson();
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(pe => pe.Name)
                .NotEmpty()
                .WithMessage($"Укажите название должности")
                .Length(settings.PositionNameMinLength, settings.PositionNameMaxLength)
                .WithMessage($"Поле должно иметь от {settings.PositionNameMinLength} до {settings.PositionNameMaxLength} символов.")
                .Must((source, name) => VerifyNameAndId(source.Id, name))
                .WithMessage($"Такая должность уже существует в базе");


            RuleFor(pe => pe.MaxNumber)
                .NotEmpty()
                .WithMessage($"Укажите максимальное количество штатных единиц")
                .InclusiveBetween(1, int.MaxValue)
                .WithMessage($"Введено некорректное значение")
                .Must((data, maxNumber) => IsValidNumber(data.Id, maxNumber))
                .WithMessage($"Количество сотрудников на данной должности превышает максимальное число штатных единиц");
        }

        private bool VerifyNameAndId(int id, string name)
        {
            return !db.Positions.Any(s => s.Name == name) || 
                db.Positions.Any(s => s.Name == name && s.Id == id);
        }

        private bool IsValidNumber(int id, int maxNumber)
        {
            var position = db.Positions
                    .Include(p => p.Appointments)
                    .AsNoTracking()
                    .FirstOrDefault(p => p.Id == id);
            return position.Appointments.Count <= maxNumber;
        }
    }
}