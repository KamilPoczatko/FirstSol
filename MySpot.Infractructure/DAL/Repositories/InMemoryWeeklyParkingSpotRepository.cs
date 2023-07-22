using MySpot.Core.Enitites;
using MySpot.Application.Services;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infractructure.DAL.Repositories
{
    public class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly IClock _clock;
        private List<WeeklyParkingSpot> _weeklyParkingSpots;


        public InMemoryWeeklyParkingSpotRepository(IClock clock)
        {
            _clock = clock;
            _weeklyParkingSpots = new List<WeeklyParkingSpot>()
            {
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P1"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5")
            };
        }

        public Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
    => Task.FromResult<IEnumerable<WeeklyParkingSpot>>(_weeklyParkingSpots);

        public Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id)
            => Task.FromResult<WeeklyParkingSpot>(_weeklyParkingSpots.SingleOrDefault(sp => sp.Id == id));

        public Task AddAsync(WeeklyParkingSpot parkingSpot)
        {
            _weeklyParkingSpots.Add(parkingSpot);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(WeeklyParkingSpot parkingSpot)
        { 
            _weeklyParkingSpots.Remove(parkingSpot);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(WeeklyParkingSpot parkingSpot)
        {
            return Task.CompletedTask;
        }
    }
}
