using System.Collections.Generic;

namespace ITAcademy.TaskTwo.Data.Models
{
    public class Position
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MaxNumber { get; set; }

        public List<EmployeePosition> Appointments { get; set; }
    }
}