using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMap
{
    public static class RegisterDIService
    {
        public static IServiceCollection AddBlazorMap(this IServiceCollection services)
        {
            services.AddScoped<IMapJsInterop, MapJsInterop>();
            return services;
        }
    }
}
