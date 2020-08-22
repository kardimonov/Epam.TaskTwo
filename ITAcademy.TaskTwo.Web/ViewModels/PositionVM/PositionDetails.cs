using System.Collections.Generic;
using ITAcademy.TaskTwo.Data.Models;

namespace ITAcademy.TaskTwo.Web.ViewModels.PositionVM
{
    public class PositionDetails
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int MaxNumber { get; set; }

        public List<Employee> Employees { get; set; }
    }
}