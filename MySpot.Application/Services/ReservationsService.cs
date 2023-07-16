using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Core.Enitites;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

        public ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
        {
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
            _clock = clock;
        }



        public async Task<ReservationDto> GetAsync(Guid id)
        {
            var reservations = await GetAllWeeklyAsync();
            return reservations.SingleOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync()
        {
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetAllAsync();
            
             return weeklyParkingSpots.SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto()
            {
                Id = x.Id,
                ParkingSpotId = x.ParkingSpotId,
                EmployeeName = x.EmployeeName,
                Date = x.Date,
            });
        }

        public async Task<Guid?> CreateAsync(CreateReservation command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpot = await _weeklyParkingSpotRepository.GetAsync(parkingSpotId);
            if(weeklyParkingSpot is null)
            {
                return default;
            }

            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, command.LicensePlate, command.Date);
            
            weeklyParkingSpot.AddReservation(reservation, _clock.Current());
            await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);


            return reservation.Id;
        }

       
        public async Task<bool> UpdateAsync(ChangeReservationLicensePlate command)
        {
            var WeeklyParkingSpot = await GetWeeklyParkingSpotByReservation(command.ReservationId);
            if(WeeklyParkingSpot is null)
            { 
                return false;
            }

            var existingReservation = WeeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);
            if (existingReservation is null)
            {
                return false;
            }

            if (existingReservation.Date.Value.Date <= _clock.Current())
            {
                return false;
            }

            existingReservation.ChangeLicensePlate(command.LicensePlate);

            await _weeklyParkingSpotRepository.UpdateAsync(WeeklyParkingSpot);

            return true;

        }
        public async Task<bool> DeleteAsync(DeleteReservation command)
        {
            var WeeklyParkingSpot = await GetWeeklyParkingSpotByReservation(command.ReservationId);
            if (WeeklyParkingSpot is null)
            {
                return false;
            }
            var existingReservation = WeeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == command.ReservationId);

            if (existingReservation is null)
            {
                return false;
            }

            WeeklyParkingSpot.RemoveReservation(command.ReservationId);
            await _weeklyParkingSpotRepository.UpdateAsync(WeeklyParkingSpot);
            return true;

        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservation(Guid reservationId)
        { 
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetAllAsync();
            var weeklyParkingSpot = weeklyParkingSpots.SingleOrDefault(x =>
            x.Reservations.Any(r => r.Id == reservationId));
            return weeklyParkingSpot;
        }
    }
}
