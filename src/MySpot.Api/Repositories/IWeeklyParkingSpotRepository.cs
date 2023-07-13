using MySpot.Api.DTO;
using MySpot.Api.Enitites;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Repositories
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
