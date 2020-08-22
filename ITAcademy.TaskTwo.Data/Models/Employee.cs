using ITAcademy.TaskTwo.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITAcademy.TaskTwo.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string SurName { get; set; }
        public DateTime Birth { get; set; }
        public string Email { get; set; }
        public MessageType Communication { get; set; }
        public int? PrimaryPhoneId { get; set; }
        public Phone PrimaryPhone { get; set; }

        public List<Message> Messages { get; set; }
        public List<EmployeeSubject> Assignments { get; set; }
        public List<EmployeePosition> Appointments { get; set; }

        [InverseProperty("Employee")]
        public List<Phone> Phones { get; set; }
    }
}