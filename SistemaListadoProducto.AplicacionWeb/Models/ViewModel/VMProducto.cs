using SistemaListadoProducto.Entity;

namespace SistemaListadoProducto.AplicacionWeb.Models.ViewModel
{
    public class VMProducto
    {
        public int IdProducto { get; set; }

        public string? CodigoBarra { get; set; }

        public string? Marca { get; set; }

        public int? Stock { get; set; }

        public int? IdCategoria { get; set; }

        public string? NombreCategoria { get; set; }

        public decimal? Precio { get; set; }

        public string? UrlImagen { get; set; }

        public string? NombreImagen { get; set; }

        public string? Descripcion { get; set; }

        public bool? esActivo { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
