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



        public ReservationDto Get(Guid id)
            => GetAllWeekly().SingleOrDefault(x => x.Id == id);


        public IEnumerable<ReservationDto> GetAllWeekly()
            => _weeklyParkingSpotRepository.GetAll().SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto()
            {
                Id = x.Id,
                ParkingSpotId = x.ParkingSpotId,
                EmployeeName = x.EmployeeName,
                Date = x.Date,
            });

        public Guid? Create(CreateReservation command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpot = _weeklyParkingSpotRepository.Get(parkingSpotId);
            if(weeklyParkingSpot is null)
            {
                return default;
            }

            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, command.LicensePlate, command.Date);
            
            weeklyParkingSpot.AddReservation(reservation, _clock.Current());

            return reservation.Id;
        }

       
        public bool Update(ChangeReservationLicensePlate command)
        {
            var WeeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
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

            return true;

        }
        public bool Delete(DeleteReservation command)
        {
            var WeeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
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
            return true;

        }

        private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid reservationId)
            => _weeklyParkingSpotRepository.GetAll().SingleOrDefault(x =>
            x.Reservations.Any(r => r.Id == reservationId));
    }
}
