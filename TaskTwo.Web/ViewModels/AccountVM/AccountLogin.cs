using System.ComponentModel.DataAnnotations;

namespace TaskTwo.Web.ViewModels.AccountVM
{
    public class AccountLogin
    {        
        [Display(Name = "Login")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}