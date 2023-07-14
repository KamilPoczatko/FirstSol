using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Enitites
{
    public class WeeklyParkingSpot
    {

        private readonly HashSet<Reservation> _reservations = new();

        public ParkingSpotId Id { get; }
        public Week Week { get; }
        public ParkingSpotName Name { get; }
        public IEnumerable<Reservation> Reservations => _reservations;

        public WeeklyParkingSpot(ParkingSpotId id, Week week, ParkingSpotName name )
        {
            Id = id;
            Week = week;
            Name = name;
        }

        public void AddReservation(Reservation reservation, DateTime now)
        {
            var isInvalidDate = reservation.Date < Week.From || 
                                reservation.Date > Week.To ||
                                reservation.Date.Value.Date < now;
            if (isInvalidDate)
            {
                throw new InvalidReservationDateException(reservation.Date.Value.Date);
            }

            var reservationAlreadyExists = Reservations.Any(x => 
                x.Date.Value.Date == reservation.Date.Value.Date);
            if (reservationAlreadyExists)
            {
                throw new ParkingSpotAlreadyReservatedException(reservation.ParkingSpotId.ToString(), reservation.Date.Value.Date);

            }

            _reservations.Add(reservation);
        }
        public void RemoveReservation(Guid Id)
        {
            var existingReservation = Reservations.SingleOrDefault(x => x.Id == Id);

            _reservations.Remove(existingReservation);
        }

    }
}
