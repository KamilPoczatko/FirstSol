using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Enitites;

namespace MySpot.Api.Services
{
    public class ReservationsService
    {
        private int _id = 1;
        static private readonly List<Reservation> reservations = new()
        {
            new Reservation{ Date = DateTime.UtcNow.AddDays(1).Date, Id = 0, EmployeeName="Roman", LicensePlate ="RMI123", ParkingSpotName = "P1" }
        };

        static private readonly List<string> _parkingSpotNames = new()
        {
            "P1", "P2", "P3", "P4", "P5"
        };

        public Reservation Get(int id) => reservations.SingleOrDefault(x => x.Id == id);


        public IEnumerable<Reservation> GetAll() => reservations;

        public int? Create(Reservation reservation)
        {
            var now = DateTime.UtcNow.Date;
            var pastDays = now.DayOfWeek is DayOfWeek.Sunday ? 7 : (int)now.DayOfWeek;
            var remainingDays = 7 - pastDays;

            if(!(reservation.Date.Date >= now && reservation.Date.Date <= now.AddDays(remainingDays))) 
            {
                return default;
            }

            if (_parkingSpotNames.All(x => x != reservation.ParkingSpotName))
            {
                return default;
            }
            reservation.Date = DateTime.UtcNow.AddDays(1).Date;

            var reservationAlreadyExists = reservations.Any(x => x.ParkingSpotName == reservation.ParkingSpotName
                                                            && x.Date.Date == reservation.Date.Date);

            if (reservationAlreadyExists)
            {
                return default;
            }

            reservation.Id = _id;
            _id++;
            reservations.Add(reservation);

            return reservation.Id;
        }

        public bool Update(int id, Reservation reservation)
        {
            var existingReservation = reservations.SingleOrDefault(x => x.Id == id);

            if (existingReservation is null)
            {
                return false;
            }
            if (existingReservation.Date.Date <= DateTime.UtcNow)
            {
                return false;
            }

            existingReservation.LicensePlate = reservation.LicensePlate;

            return true;

        }

        public bool Delete(int id)
        {
            var existingReservation = reservations.SingleOrDefault(x => x.Id == id);

            if (existingReservation is null)
            {
                return false;
            }

            reservations.Remove(existingReservation);
            return true;

        }
    }
}
