using System.Collections.Generic;

namespace ITAcademy.TaskTwo.Data.Models
{
    public class Subject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<EmployeeSubject> Assignments { get; set; }
    }
}