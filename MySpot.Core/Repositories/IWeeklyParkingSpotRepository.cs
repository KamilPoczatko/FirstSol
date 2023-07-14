using MySpot.Core.Enitites;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        IEnumerable<WeeklyParkingSpot> GetAll();
        WeeklyParkingSpot Get(ParkingSpotId id);
        void Add(WeeklyParkingSpot parkingSpot);
        void Update(WeeklyParkingSpot parkingSpot);
        void Delete(WeeklyParkingSpot parkingSpot);


    }
}
