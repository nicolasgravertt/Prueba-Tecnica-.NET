using SistemaListadoProducto.BLL.Interfaces;
using SistemaListadoProducto.DAL.Interfaces;
using SistemaListadoProducto.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaListadoProducto.BLL.Implementacion
{
    public class ProductoService : IProductoService
    {
        private readonly IGenericRepository<Producto> _repositorio;
        private readonly IUtilidadesService _utilidadesService;

        public ProductoService(IGenericRepository<Producto> repositorio, IUtilidadesService utilidadesService)
        {
            _repositorio = repositorio;
            _utilidadesService = utilidadesService;
        }

        public async Task<Producto> Crear(Producto entidad)
        {
            try
            {
                Producto producto_creado = await _repositorio.Crear(entidad);

                if (producto_creado.IdProducto == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el usuario");
                }

                return producto_creado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Producto> Editar(Producto entidad)
        {
            Producto producto_existe = await _repositorio.Obtener(u => u.IdProducto == entidad.IdProducto);
            if (producto_existe == null)
            {
                throw new TaskCanceledException("No se encontro el usuario");
            }

            try
            {
                IQueryable<Producto> queryProducto = await _repositorio.Consultar(u => u.IdProducto == entidad.IdProducto);

                Producto producto_editar = queryProducto.First();
                producto_editar.Nombre = entidad.Nombre;
                producto_editar.Marca = entidad.Marca;
                producto_editar.IdCategoria = entidad.IdCategoria;
                producto_editar.Precio = entidad.Precio;
                producto_editar.UrlImagen = entidad.UrlImagen;
                producto_editar.Descripcion = entidad.Descripcion;
                producto_editar.IsActive = entidad.IsActive;

                bool respuesta = await _repositorio.Editar(producto_editar);

                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo modificar el producto");
                }

                return producto_editar;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idProducto)
        {
            try
            {
                Producto producto_encontrado = await _repositorio.Obtener(u => u.IdProducto == idProducto);
                if (producto_encontrado == null)
                {
                    throw new TaskCanceledException("El producto no existe");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Producto>> Lista()
        {
            IQueryable<Producto> query = await _repositorio.Consultar();
            return query.ToList();
        }
    }
}
