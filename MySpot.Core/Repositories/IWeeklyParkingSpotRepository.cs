using MySpot.Core.Enitites;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync();
        Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id);
        Task AddAsync(WeeklyParkingSpot parkingSpot);
        Task UpdateAsync(WeeklyParkingSpot parkingSpot);
        Task DeleteAsync(WeeklyParkingSpot parkingSpot);


    }
}
