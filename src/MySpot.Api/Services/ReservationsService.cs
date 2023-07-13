using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Enitites;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private static readonly Clock Clock = new Clock();


        
        private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

        public ReservationsService(List<WeeklyParkingSpot> weeklyParkingSpots)
        {
            _weeklyParkingSpots = weeklyParkingSpots;                
        }



        public ReservationDto Get(Guid id)
            => GetAllWeekly().SingleOrDefault(x => x.Id == id);


        public IEnumerable<ReservationDto> GetAllWeekly()
            => _weeklyParkingSpots.SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto()
            {
                Id = x.Id,
                ParkingSpotId = x.ParkingSpotId,
                EmployeeName = x.EmployeeName,
                Date = x.Date,
            });

        public Guid? Create(CreateReservation command)
        {

            var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);
            if(weeklyParkingSpot is null)
            {
                return default;
            }

            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, command.LicensePlate, command.Date);
            
            weeklyParkingSpot.AddReservation(reservation, Clock.Current());

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

            if (existingReservation.Date.Value.Date <= Clock.Current())
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
            => _weeklyParkingSpots.SingleOrDefault(x =>
            x.Reservations.Any(r => r.Id == reservationId));
    }
}
