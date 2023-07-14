using MySpot.Core.Enitites;
using MySpot.Application.Services;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Infractructure.Repositories
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

        public IEnumerable<WeeklyParkingSpot> GetAll()
    => _weeklyParkingSpots;

        public WeeklyParkingSpot Get(ParkingSpotId id)
            => _weeklyParkingSpots.SingleOrDefault(sp => sp.Id == id);

        public void Add(WeeklyParkingSpot parkingSpot)
        {
            _weeklyParkingSpots.Add(parkingSpot);
        }

        public void Delete(WeeklyParkingSpot parkingSpot)
        => _weeklyParkingSpots.Remove(parkingSpot);  

        public void Update(WeeklyParkingSpot parkingSpot)
        {
        }
    }
}
