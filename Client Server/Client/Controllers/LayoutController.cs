using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class LayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
