using Microsoft.AspNetCore.Mvc;

namespace SistemaListadoProducto.AplicacionWeb.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
