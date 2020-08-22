using System.ComponentModel.DataAnnotations;

namespace ITAcademy.TaskTwo.Web.ViewModels.PhoneVM
{
    public class PhoneCreate
    {
        [Display(Name = "Номер телефона")]
        public string Number { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeSurName { get; set; }

        public string EmployeeFirstName { get; set; }

        public string EmployeeSecondName { get; set; }

        public string Method { get; set; }
    }
}