using System.Collections.Generic;

namespace TaskTwo.Logic.Models.SubjectDTO
{
    public class SubjectWithEmployeesDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<AssignedEmployeeDto> Employees { get; set; }
    }
}
