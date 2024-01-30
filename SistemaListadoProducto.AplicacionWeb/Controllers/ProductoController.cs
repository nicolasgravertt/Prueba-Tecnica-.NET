using Microsoft.AspNetCore.Mvc;

namespace SistemaListadoProducto.AplicacionWeb.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
