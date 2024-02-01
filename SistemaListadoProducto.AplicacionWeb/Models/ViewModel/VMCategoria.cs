using SistemaListadoProducto.Entity;

namespace SistemaListadoProducto.AplicacionWeb.Models.ViewModel
{
    public class VMCategoria
    {
        public int IdCategoria { get; set; }

        public string? Descripcion { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
