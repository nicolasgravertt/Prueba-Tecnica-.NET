using SistemaListadoProducto.AplicacionWeb.Models.ViewModel;
using SistemaListadoProducto.Entity;
using System.Globalization;
using AutoMapper;

namespace SistemaListadoProducto.AplicacionWeb.Utilidades.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Categoria
            CreateMap<Categoria, VMCategoria>().ReverseMap();
            CreateMap<VMCategoria, Categoria> ().ReverseMap();
            #endregion Categoria
            #region Usuario
            CreateMap<Usuario, VMUsuario>().ReverseMap();
            #endregion Usuario
            #region Producto
            CreateMap<Producto, VMProducto>().ReverseMap();
            #endregion Producto
        }
    }
}
