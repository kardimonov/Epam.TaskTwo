using System.Collections.Generic;

namespace ITAcademy.TaskTwo.Logic.Models.PositionDTO
{
    public class PositionWithEmployees
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MaxNumber { get; set; }

        public List<AppointedEmployee> AllEmployees { get; set; }
    }
}