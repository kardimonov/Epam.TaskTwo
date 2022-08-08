using System.Collections.Generic;

namespace TaskTwo.Logic.Models.SubjectDTO
{
    public class SubjectWithEmployees
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<AssignedEmployee> AllEmployees { get; set; }
    }
}