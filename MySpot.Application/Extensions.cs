using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Services;

namespace MySpot.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IClock, Clock>();
            services.AddScoped<IReservationsService, ReservationsService>();
            return services;
        }
    }
}
