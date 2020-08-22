namespace ITAcademy.TaskTwo.Data.Models
{
    public class Phone
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}