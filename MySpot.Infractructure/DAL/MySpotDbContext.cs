using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySpot.Core.Enitites;

namespace MySpot.Infractructure.DAL
{
    public sealed class MySpotDbContext : DbContext
    {
        public DbSet<WeeklyParkingSpot> WeeklyParkingSpots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public MySpotDbContext(DbContextOptions<MySpotDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
