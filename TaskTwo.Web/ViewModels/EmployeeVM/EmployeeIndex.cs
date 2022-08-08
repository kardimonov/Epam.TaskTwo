using System;
using System.Collections.Generic;

namespace TaskTwo.Web.ViewModels.EmployeeVM
{
    public class EmployeeIndex
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