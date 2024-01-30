﻿using System;
using System.Collections.Generic;

namespace SistemaListadoProducto.Entity;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? Nombre { get; set; }

    public string? Marca { get; set; }

    public int? IdCategoria { get; set; }

    public decimal? Precio { get; set; }

    public string? UrlImagen { get; set; }

    public string? Descripcion { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }
}