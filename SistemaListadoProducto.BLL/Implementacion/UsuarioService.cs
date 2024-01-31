using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Net;
using SistemaListadoProducto.BLL.Interfaces;
using SistemaListadoProducto.DAL.Interfaces;
using SistemaListadoProducto.Entity;

namespace SistemaListadoProducto.BLL.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<Usuario> _repositorio;
        private readonly IUtilidadesService _utilidadesService;

        public UsuarioService(
            IGenericRepository<Usuario> repositorio,
            IUtilidadesService utilidadesService
        )
        {
            _repositorio = repositorio;
            _utilidadesService = utilidadesService;
        }
        public async Task<List<Usuario>> Lista()
        {
            IQueryable<Usuario> query = await _repositorio.Consultar();
            return query.ToList();
        }
        public async Task<Usuario> Crear(Usuario entidad, Stream foto, string nombreFoto = "", string urlPlantillaCorreo = "")
        {
            Usuario usuario_existe = await _repositorio.Obtener(u => u.Correo == entidad.Correo);
            if (usuario_existe != null)
            {
                throw new TaskCanceledException("El correo ya existe");
            }

            try
            {
                string clave_generada = _utilidadesService.GenerarClave();
                entidad.Clave = _utilidadesService.ConvertirSha256(clave_generada);

                Usuario usuario_creado = await _repositorio.Crear(entidad);

                if (usuario_creado.IdUsuario == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el usuario");
                }

                return usuario_creado;

            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<Usuario> Editar(Usuario entidad, Stream foto, string nombreFoto = "")
        {
            Usuario usuario_existe = await _repositorio.Obtener(u => u.Correo == entidad.Correo && u.IdUsuario != entidad.IdUsuario);
            if (usuario_existe != null)
            {
                throw new TaskCanceledException("El correo ya existe");
            }

            try
            {
                IQueryable<Usuario> queryUsuario = await _repositorio.Consultar(u => u.IdUsuario == entidad.IdUsuario);

                Usuario usuario_editar =  queryUsuario.First();
                usuario_editar.Nombre = entidad.Nombre;
                usuario_editar.Correo = entidad.Correo;
                usuario_editar.Clave = entidad.Clave;
                usuario_editar.Apellido = entidad.Apellido;

                bool respuesta = await _repositorio.Editar(usuario_editar);

                if (!respuesta)
                {
                    throw new TaskCanceledException("No se pudo modificar el usuario");
                }

                return usuario_editar;

            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> Eliminar(int idUsuario)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == idUsuario);
                if (usuario_encontrado == null)
                {
                    throw new TaskCanceledException("El usuario no existe");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Usuario> ObtenerPorCredenciales(string correo, string clave)
        {
            string clave_encriptada = _utilidadesService.ConvertirSha256(clave);
            Usuario usuario_encontrado = await _repositorio.Obtener(u => u.Correo.Equals(correo) && u.Clave.Equals(clave_encriptada));
            return usuario_encontrado;
        }
        public async Task<Usuario> ObtenerPorId(int idUsuario)
        {
            IQueryable<Usuario> query = await _repositorio.Consultar(u => u.IdUsuario == idUsuario);
            return query.First();
        }
        public async Task<bool> GuardarPerfil(Usuario entidad)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == entidad.IdUsuario);
                if(usuario_encontrado == null)
                {
                    throw new TaskCanceledException("El usuario no existe");
                }

                usuario_encontrado.Correo = entidad.Correo;
                usuario_encontrado.Nombre = entidad.Nombre;
                usuario_encontrado.Apellido = entidad.Apellido;

                bool respuesta = await _repositorio.Editar(usuario_encontrado);
                return respuesta;

            }catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> CambiarClave(int idUsuario, string claveActual, string claveNueva)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.IdUsuario == idUsuario);
                if(usuario_encontrado == null)
                {
                    throw new TaskCanceledException("El usuario no existe");
                }
                if (usuario_encontrado.Clave != _utilidadesService.ConvertirSha256(claveActual))
                {
                    throw new TaskCanceledException("La contraseña ingresada no es correcta");
                }

                usuario_encontrado.Clave = _utilidadesService.ConvertirSha256(claveNueva);

                bool respuesta = await _repositorio.Editar(usuario_encontrado);

                return respuesta;

            }catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RestablecerClave(string correo, string urlPlantillaCorreo)
        {
            try
            {
                Usuario usuario_encontrado = await _repositorio.Obtener(u => u.Correo == correo);
                if( usuario_encontrado == null) { throw new TaskCanceledException("usuario no encontrado"); }
                string clave_generada = _utilidadesService.GenerarClave();
                usuario_encontrado.Clave = _utilidadesService.ConvertirSha256(clave_generada);

                bool respuesta = await _repositorio.Editar(usuario_encontrado);
                return respuesta;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
