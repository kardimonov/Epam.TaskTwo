namespace TaskTwo.Data.Models
{
    public class EmployeeSubject
    {
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }
    }
}