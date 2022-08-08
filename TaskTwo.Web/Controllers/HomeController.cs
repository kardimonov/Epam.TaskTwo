using System;
using System.Diagnostics;
using TaskTwo.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace TaskTwo.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return (User.Identity.IsAuthenticated) ?
                RedirectToActionPermanent("Index", "Employee") :
                RedirectToActionPermanent("Authenticate", "Account");
        }

        public void Exceptions()
        {
            throw new Exception("Test");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var result = new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };
            return View(result);
        }
    }
}