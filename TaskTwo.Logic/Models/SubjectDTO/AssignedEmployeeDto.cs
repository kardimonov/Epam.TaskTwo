using System.Collections.Generic;

namespace TaskTwo.Logic.Models.SubjectDTO
{
    public class AssignedEmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string SurName { get; set; }

        public string Email { get; set; }

        public List<string> Phones { get; set; }
    }
}