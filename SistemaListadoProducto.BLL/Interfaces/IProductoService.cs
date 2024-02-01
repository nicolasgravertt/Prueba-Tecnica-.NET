using SistemaListadoProducto.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaListadoProducto.BLL.Interfaces
{
    internal interface IProductoService
    {
        Task<List<Producto>> Lista();
        Task<Producto> Crear(Producto entidad);

        Task<Producto> Editar(Producto entidad);

        Task<bool> Eliminar(int idProducto);
    }
}
