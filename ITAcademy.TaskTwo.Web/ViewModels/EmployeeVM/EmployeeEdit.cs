using ITAcademy.TaskTwo.Data.Enums;
using ITAcademy.TaskTwo.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITAcademy.TaskTwo.Web.ViewModels.EmployeeVM
{
    public class EmployeeEdit
    {
        public int Id { get; set; }

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

        public int? PrimaryPhoneId { get; set; }

        public List<Phone> Phones { get; set; }
    }
}