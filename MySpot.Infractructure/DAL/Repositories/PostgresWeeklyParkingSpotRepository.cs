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
        public async Task AddAsync(WeeklyParkingSpot parkingSpot)
        { 
            await _dbContext.WeeklyParkingSpots.AddAsync(parkingSpot);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(WeeklyParkingSpot parkingSpot)
        {
            _dbContext.WeeklyParkingSpots.Remove(parkingSpot);
            await _dbContext.SaveChangesAsync();
        }
        public Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id)
            => _dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)    
            .SingleOrDefaultAsync(s => s.Id == id);

        public async Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
        {
            var result = await _dbContext.WeeklyParkingSpots
            .Include(x => x.Reservations)
            .ToListAsync();

            return result.AsEnumerable();
        }

        public async Task UpdateAsync(WeeklyParkingSpot parkingSpot)
        {
            _dbContext.WeeklyParkingSpots.Update(parkingSpot);
            await _dbContext.SaveChangesAsync();
        }
    }
}
