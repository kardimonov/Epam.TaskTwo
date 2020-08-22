using FluentValidation;
using ITAcademy.TaskTwo.Logic.Models.PositionDTO;

namespace ITAcademy.TaskTwo.Web.Validators
{
    public class PositionEditEmployeesValidator : AbstractValidator<PositionWithEmployees>
    {
        public PositionEditEmployeesValidator()
        {
            RuleFor(pee => pee)
                .Must(ValidAppointment)
                .WithMessage(
                $"Количество сотрудников на данной должности превысило установленный лимит");
        }

        private bool ValidAppointment(PositionWithEmployees source)
        {
            var appointedNumber = 0;
            foreach (var employee in source.AllEmployees)
            {
                if (employee.Appointed)
                {
                    appointedNumber++;
                }
            }
            return appointedNumber <= source.MaxNumber;
        }
    }
}