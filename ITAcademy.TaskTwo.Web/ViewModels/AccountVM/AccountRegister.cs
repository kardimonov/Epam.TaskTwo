using System.ComponentModel.DataAnnotations;

namespace ITAcademy.TaskTwo.Web.ViewModels.AccountVM
{
    public class AccountRegister
    {        
        [Display(Name = "Login")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
                
        [DataType(DataType.Password)]        
        [Display(Name = "Пароль")]
        public string Password { get; set; }
                
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}