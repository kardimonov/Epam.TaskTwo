using System.Collections.Generic;

namespace ITAcademy.TaskTwo.Logic.Models.PositionDTO
{
    public class PositionWithEmployeesDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumberOfVacancies { get; set; }

        public List<AppointedEmployeeDto> Employees { get; set; }
    }
}
