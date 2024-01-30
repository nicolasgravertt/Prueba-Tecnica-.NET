using Microsoft.AspNetCore.Mvc;

namespace SistemaListadoProducto.AplicacionWeb.Controllers
{
    public class NegocioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
