using Microsoft.AspNetCore.Mvc;

namespace TaskTwo.Web.ViewComponents
{
    public class Register : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}