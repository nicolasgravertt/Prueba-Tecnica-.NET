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
            #endregion Categoria
            #region Usuario
            CreateMap<Usuario, VMUsuario>().ReverseMap();
            #endregion Usuario
            #region Producto
            CreateMap<Producto, VMProducto>().ForMember(destino =>
                destino.NombreCategoria,
                opt => opt.MapFrom(origen => origen.IdCategoriaNavigation.Descripcion)
            ).ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-MX")))
            );

            CreateMap<VMProducto,Producto>().ForMember(destino =>
                destino.IdCategoriaNavigation,
                opt => opt.Ignore()
            ).ForMember(destino =>
                destino.Precio,
                opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-MX")))
            );
            #endregion Producto
        }
    }
}
