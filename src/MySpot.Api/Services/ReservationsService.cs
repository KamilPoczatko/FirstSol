using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Enitites;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {


        static private readonly List<WeeklyParkingSpot> WeeklyParkingSpots = new()
        {
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001") , DateTime.UtcNow.Date, DateTime.UtcNow.Date.AddDays(7), "P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002") , DateTime.UtcNow.Date, DateTime.UtcNow.Date.AddDays(7), "P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003") , DateTime.UtcNow.Date, DateTime.UtcNow.Date.AddDays(7), "P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004") , DateTime.UtcNow.Date, DateTime.UtcNow.Date.AddDays(7), "P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005") , DateTime.UtcNow.Date, DateTime.UtcNow.Date.AddDays(7), "P5")
        };





        public ReservationDto Get(Guid id)
            => GetAllWeekly().SingleOrDefault(x => x.Id == id);


        public IEnumerable<ReservationDto> GetAllWeekly()
            => WeeklyParkingSpots.SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto()
            { 
                Id = x.Id,
                ParkingSpotId = x.ParkingSpotId,
                EmployeeName = x.EmployeeName,
                Date = x.Date,
            });

        public Guid? Create(CreateReservation command)
        {

            var weeklyParkingSpot = WeeklyParkingSpots.SingleOrDefault(x => x.Id == command.ParkingSpotId);
            if(weeklyParkingSpot is null)
            {
                return default;
            }

            var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, command.LicensePlate, command.Date);
            
            weeklyParkingSpot.AddReservation(reservation);

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

            if (existingReservation.Date.Date <= DateTime.UtcNow)
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
            => WeeklyParkingSpots.SingleOrDefault(x =>
            x.Reservations.Any(r => r.Id == reservationId));
    }
}
