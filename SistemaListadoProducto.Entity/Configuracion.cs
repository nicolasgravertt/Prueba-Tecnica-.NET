using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaListadoProducto.Entity
{
    public partial class Configuracion
    {
        public int IdConfiguracion { get; set; }

        public string? Recurso { get; set; }

        public string? Propiedad { get; set; }

        public string? Valor { get; set; }

    }
}
