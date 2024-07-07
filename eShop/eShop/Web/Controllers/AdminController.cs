using Microsoft.AspNetCore.Mvc;

namespace eShop.Web.Controllers
{
    public class AdminController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}
