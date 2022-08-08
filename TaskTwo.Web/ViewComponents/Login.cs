using TaskTwo.Web.ViewModels.AccountVM;
using Microsoft.AspNetCore.Mvc;

namespace TaskTwo.Web.ViewComponents
{
    public class Login : ViewComponent
    {        
        public IViewComponentResult Invoke(string returnUrl = null)
        {
            return View(new AccountLogin { ReturnUrl = returnUrl });
        }        
    }
}