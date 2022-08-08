using System.ComponentModel.DataAnnotations;

namespace TaskTwo.Web.ViewModels.PhoneVM
{
    public class PhoneEdit
    {
        public int Id { get; set; }

        [Display(Name = "Номер телефона")]
        public string Number { get; set; }

        public int EmployeeId { get; set; }

        public string Method { get; set; }
    }
}