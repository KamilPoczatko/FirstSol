using Microsoft.EntityFrameworkCore;
using MySpot.Core.Enitites;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpot.Infractructure.DAL.Repositories
{
    internal sealed class PostgresWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly MySpotDbContext _dbContext;

        public PostgresWeeklyParkingSpotRepository(MySpotDbContext dbContext)
        {
            _dbContext = dbContext;    
        }
        public void Add(WeeklyParkingSpot parkingSpot)
        { 
            _dbContext.WeeklyParkingSpots.Add(parkingSpot);
            _dbContext.SaveChanges();
        }
        public void Delete(WeeklyParkingSpot parkingSpot)
        {
            _dbContext.WeeklyParkingSpots.Remove(parkingSpot);
            _dbContext.SaveChanges();
        }
        public WeeklyParkingSpot Get(ParkingSpotId id)
            => _dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)    
            .SingleOrDefault(s => s.Id == id);

        public IEnumerable<WeeklyParkingSpot> GetAll()
            => _dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations);

        public void Update(WeeklyParkingSpot parkingSpot)
        {
            _dbContext.WeeklyParkingSpots.Update(parkingSpot);
            _dbContext.SaveChanges();
        }
    }
}
