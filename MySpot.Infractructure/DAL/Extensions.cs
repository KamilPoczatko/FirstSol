using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MySpot.Core.Repositories;
using MySpot.Infractructure.DAL.Repositories;

namespace MySpot.Infractructure.DAL
{
    internal static class Extensions
    {
        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            //const string connectionString = "Host=localhost;Database=MySpot;Username=postgres;Password=";
            var section = configuration.GetSection("DataBase");
            services.Configure<DataBaseOptions>(section);

            var options = new DataBaseOptions();
            section.Bind(options);

            services.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(options.ConnectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior",true);
            services.AddHostedService<DatabaseInitializer>();
            return services;
        }
    }
}
