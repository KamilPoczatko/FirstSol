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
        private const string SectionName = "DataBase";

        public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection(SectionName);
            services.Configure<DataBaseOptions>(section);

            var options = GetOptions<DataBaseOptions>(configuration, SectionName);

            services.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(options.ConnectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            //services.AddSingleton<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddHostedService<DatabaseInitializer>();
            return services;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetSection(sectionName);
                        
            section.Bind(options);
            return options;
        }
    }
}
