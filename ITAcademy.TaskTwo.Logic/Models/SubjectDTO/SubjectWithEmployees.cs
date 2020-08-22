using System.Collections.Generic;

namespace ITAcademy.TaskTwo.Logic.Models.SubjectDTO
{
    public class SubjectWithEmployees
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<AssignedEmployee> AllEmployees { get; set; }
    }
}