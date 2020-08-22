using Microsoft.AspNetCore.Mvc;

namespace ITAcademy.TaskTwo.Web.ViewComponents
{
    public class Register : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}