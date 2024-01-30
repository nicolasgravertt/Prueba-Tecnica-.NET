using Microsoft.AspNetCore.Mvc;

namespace SistemaListadoProducto.AplicacionWeb.Controllers
{
    public class ReporteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
