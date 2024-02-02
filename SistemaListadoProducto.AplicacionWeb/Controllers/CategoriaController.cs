using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using Newtonsoft.Json;
using SistemaListadoProducto.AplicacionWeb.Models.ViewModel;
using SistemaListadoProducto.AplicacionWeb.Utilidades.Response;
using SistemaListadoProducto.BLL.Interfaces;
using SistemaListadoProducto.Entity;

namespace SistemaListadoProducto.AplicacionWeb.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaService categoriaService, IMapper mapper)
        {
            _categoriaService = categoriaService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaCategoria()
        {
            List<VMCategoria> vmCategoriaLista = _mapper.Map<List<VMCategoria>>(await _categoriaService.Lista());
            return StatusCode(StatusCodes.Status200OK, new { data = vmCategoriaLista });
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromForm] string modelo)
        {
            GenericResponse<VMCategoria> gResponse = new GenericResponse<VMCategoria>();
            try
            {
                VMCategoria vmCategoria = JsonConvert.DeserializeObject<VMCategoria>(modelo);

                Categoria categoria_creada = await _categoriaService.Crear(_mapper.Map<Categoria>(vmCategoria));

                vmCategoria = _mapper.Map<VMCategoria>(categoria_creada);

                gResponse.Estado = true;
                gResponse.Objeto = vmCategoria;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromForm] string modelo)
        {
            GenericResponse<VMCategoria> gResponse = new GenericResponse<VMCategoria>();
            try
            {
                VMCategoria vmCategoria = JsonConvert.DeserializeObject<VMCategoria>(modelo);

                Categoria categoria_editada = await _categoriaService.Editar(_mapper.Map<Categoria>(vmCategoria));

                vmCategoria = _mapper.Map<VMCategoria>(categoria_editada);

                gResponse.Estado = true;
                gResponse.Objeto = vmCategoria;

            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int idCategoria)
        {
            GenericResponse<string> gResponse = new GenericResponse<string>();
            try
            {
                gResponse.Estado = await _categoriaService.Eliminar(idCategoria);
            }
            catch (Exception ex)
            {
                gResponse.Estado = false;
                gResponse.Mensaje = ex.Message;
            }
            return StatusCode(StatusCodes.Status200OK, gResponse);

        }
    }
}
