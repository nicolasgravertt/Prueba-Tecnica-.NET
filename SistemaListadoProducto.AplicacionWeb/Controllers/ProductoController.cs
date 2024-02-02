using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaListadoProducto.AplicacionWeb.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
