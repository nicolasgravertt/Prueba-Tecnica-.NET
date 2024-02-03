using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SistemaListadoProducto.Entity;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? CodigoBarra { get; set; }

    public string? Marca { get; set; }

    public int? Stock { get; set; }

    public int? IdCategoria { get; set; }

    public decimal? Precio { get; set; }

    public string? UrlImagen { get; set; }

    public string? NombreImagen { get; set; }

    public string? Descripcion { get; set; }

    public bool? esActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }
}
