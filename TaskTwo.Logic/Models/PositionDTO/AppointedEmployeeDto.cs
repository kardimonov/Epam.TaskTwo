using System;
using System.Collections.Generic;

namespace TaskTwo.Logic.Models.PositionDTO
{
    public class AppointedEmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string SurName { get; set; }

        public DateTime Birth { get; set; }

        public string Email { get; set; }
        
        public List<string> Phones { get; set; }

        public List<string> Subjects { get; set; }
    }
}