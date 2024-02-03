﻿using Microsoft.EntityFrameworkCore;
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
        private readonly IFireBaseService _fireBaseService;

        public ProductoService(IGenericRepository<Producto> repositorio, IFireBaseService fireBaseService)
        {
            _repositorio = repositorio;
            _fireBaseService = fireBaseService;
        }

        public async Task<Producto> Crear(Producto entidad, Stream imagen = null, string NombreImagen = "")
        {
            Producto producto_existe = await _repositorio.Obtener(p=>p.CodigoBarra == entidad.CodigoBarra);
            if (producto_existe != null)
                throw new TaskCanceledException("El codigo de barra ya existe");

            try
            {
                entidad.NombreImagen = NombreImagen;
                if(imagen != null)
                {
                    string urlImagen = await _fireBaseService.SubirStorage(imagen, "carpeta_producto", NombreImagen);
                    entidad.UrlImagen = urlImagen;
                }

                Producto producto_creado = await _repositorio.Crear(entidad);

                if (producto_creado.IdProducto == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el producto");
                }

                IQueryable<Producto> query = await _repositorio.Consultar(p => p.IdProducto == producto_creado.IdProducto);
                producto_creado = query.Include(c => c.IdCategoriaNavigation).First();

                return producto_creado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Producto> Editar(Producto entidad, Stream imagen = null, string nombreImagen = "")
        {
            Producto producto_existe = await _repositorio.Obtener(u => u.CodigoBarra == entidad.CodigoBarra && u.IdProducto != entidad.IdProducto);
            if (producto_existe != null)
            {
                throw new TaskCanceledException("El codigo de barra ya existe");
            }

            try
            {
                IQueryable<Producto> queryProducto = await _repositorio.Consultar(u => u.IdProducto == entidad.IdProducto);

                Producto producto_editar = queryProducto.First();
                producto_editar.CodigoBarra = entidad.CodigoBarra;
                producto_editar.Marca = entidad.Marca;
                producto_editar.Descripcion = entidad.Descripcion;
                producto_editar.IdCategoria = entidad.IdCategoria;
                producto_editar.Stock = entidad.Stock;
                producto_editar.Precio = entidad.Precio;
                producto_editar.esActivo = entidad.esActivo;

                if(producto_editar.NombreImagen == "")
                {
                    producto_editar.NombreImagen = nombreImagen;
                }

                if(imagen != null)
                {
                    string urlImagen = await _fireBaseService.SubirStorage(imagen, "carpeta_producto", producto_editar.NombreImagen);
                    producto_editar.UrlImagen = urlImagen;
                }

                bool respuesta = await _repositorio.Editar(producto_editar);

                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo modificar el producto");
                }

                Producto producto_editado = queryProducto.Include(c => c.IdCategoriaNavigation).First();

                return producto_editado;

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
                Producto producto_encontrado = await _repositorio.Obtener(p => p.IdProducto == idProducto);
                if (producto_encontrado == null)
                {
                    throw new TaskCanceledException("El producto no existe");
                }

                string nombreImagen = producto_encontrado.NombreImagen;

                bool respuesta = await _repositorio.Eliminar(producto_encontrado);

                if (respuesta)
                    await _fireBaseService.EliminarStorage("carpeta_producto", nombreImagen);

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
            return query.Include(c => c.IdCategoriaNavigation).ToList();
        }
    }
}
