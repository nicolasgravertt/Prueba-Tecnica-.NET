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
    public class CategoriaService : ICategoriaService
    {
        private readonly IGenericRepository<Categoria> _repositorio;
        private readonly IUtilidadesService _utilidadesService;

        public CategoriaService(IGenericRepository<Categoria> repositorio, IUtilidadesService utilidadesService)
        {
            _repositorio = repositorio;
            _utilidadesService = utilidadesService;
        }

        public async Task<Categoria> Crear(Categoria entidad)
        {
            try
            {
                Categoria categoria_creada = await _repositorio.Crear(entidad);

                if (categoria_creada.IdCategoria == 0)
                {
                    throw new TaskCanceledException("No se pudo crear la categoria");
                }

                return categoria_creada;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Categoria> Editar(Categoria entidad)
        {
            Categoria categoria_existe = await _repositorio.Obtener(u => u.IdCategoria == entidad.IdCategoria);
            if (categoria_existe == null)
            {
                throw new TaskCanceledException("No se encontro la categoria");
            }

            try
            {
                IQueryable<Categoria> queryCategoria = await _repositorio.Consultar(u => u.IdCategoria == entidad.IdCategoria);

                Categoria categoria_editar = queryCategoria.First();
                categoria_editar.Descripcion = entidad.Descripcion;
                categoria_editar.IsActive = entidad.IsActive;

                bool respuesta = await _repositorio.Editar(categoria_editar);

                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo modificar la categoria");
                }

                return categoria_editar;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int idCategoria)
        {
            try
            {
                Categoria categoria_encontrada = await _repositorio.Obtener(u => u.IdCategoria == idCategoria);
                if (categoria_encontrada == null)
                {
                    throw new TaskCanceledException("La categoria no existe");
                }

                bool respuesta = await _repositorio.Eliminar(categoria_encontrada);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Categoria>> Lista()
        {
            IQueryable<Categoria> query = await _repositorio.Consultar();
            return query.ToList();
        }
    }
}
