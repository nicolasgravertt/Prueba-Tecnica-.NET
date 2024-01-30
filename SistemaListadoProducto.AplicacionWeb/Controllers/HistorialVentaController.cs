using Microsoft.AspNetCore.Mvc;

namespace SistemaListadoProducto.AplicacionWeb.Controllers
{
    public class HistorialVentaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
