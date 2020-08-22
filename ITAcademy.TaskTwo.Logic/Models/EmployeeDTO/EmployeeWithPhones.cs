using System;
using System.Collections.Generic;

namespace ITAcademy.TaskTwo.Logic.Models.EmployeeDTO
{
    public class EmployeeWithPhones
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string SurName { get; set; }

        public DateTime Birth { get; set; }

        public string Email { get; set; }

        public List<string> Phones { get; set; }
    }
}