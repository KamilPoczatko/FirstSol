using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Infractructure.Repositories;

namespace MySpot.Infractructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<InMemoryWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>();
            return services;
        }
    }
}
