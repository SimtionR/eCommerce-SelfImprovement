using Microsoft.Extensions.DependencyInjection;
using Self_Improve_eCommerce.IServices;
using Self_Improve_eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Self_Improve_eCommerce.Configuration
{
    public static class ServiceConfiguration
    {
       public static IServiceCollection AddApplicationServices( IServiceCollection services)
        {
            return services
                .AddTransient<IProductService, ProductService>();
        }
    }
}
