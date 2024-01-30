using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaListadoProducto.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
//using SistemaListadoProducto.DAL.Implementacion;
//using SistemaListadoProducto.DAL.Interfaces;
//using SistemaListadoProducto.BLL.Implementacion;
//using SistemaListadoProducto.BLL.Interfaces;

namespace SistemaListadoProducto.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<PruebaContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CadenaSQL"));
            });
        }
    }
}
