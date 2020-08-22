using ITAcademy.TaskTwo.Web.ViewModels.AccountVM;
using Microsoft.AspNetCore.Mvc;

namespace ITAcademy.TaskTwo.Web.ViewComponents
{
    public class Login : ViewComponent
    {        
        public IViewComponentResult Invoke(string returnUrl = null)
        {
            return View(new AccountLogin { ReturnUrl = returnUrl });
        }        
    }
}