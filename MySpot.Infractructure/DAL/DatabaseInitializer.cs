using Microsoft.EntityFrameworkCore;
using MySpot.Application.Services;
using MySpot.Core.Enitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.ValueObjects;

namespace MySpot.Infractructure.DAL
{
    internal sealed class DatabaseInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public DatabaseInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scoper = _serviceProvider.CreateScope())
            {
                var dbContext = scoper.ServiceProvider.GetRequiredService<MySpotDbContext>();
                dbContext.Database.Migrate();

                var weeklyParkingSpots = dbContext.WeeklyParkingSpots.ToList();

                if (!weeklyParkingSpots.Any())
                {
                    var clock = new Clock();
                    weeklyParkingSpots = new List<WeeklyParkingSpot>()
                    {
                        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1"),
                        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P2"),
                        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3"),
                        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P4"),
                        new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P5")
                    };
                    dbContext.WeeklyParkingSpots.AddRange(weeklyParkingSpots);
                    dbContext.SaveChanges();
                }
            }
            return Task.CompletedTask; 
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
