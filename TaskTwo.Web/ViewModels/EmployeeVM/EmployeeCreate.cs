using TaskTwo.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace TaskTwo.Web.ViewModels.EmployeeVM
{
    public class EmployeeCreate
    {
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Отчество")]
        public string SecondName { get; set; }

        [Display(Name = "Фамилия")]
        public string SurName { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime Birth { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Предпочитаемый способ коммуникации")]
        public MessageType Communication { get; set; }
    }
}