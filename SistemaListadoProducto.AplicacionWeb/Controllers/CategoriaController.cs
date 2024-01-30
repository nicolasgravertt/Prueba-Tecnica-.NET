using Microsoft.AspNetCore.Mvc;

namespace SistemaListadoProducto.AplicacionWeb.Controllers
{
    public class CategoriaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
