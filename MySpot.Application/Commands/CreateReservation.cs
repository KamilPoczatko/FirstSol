using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands
{
    public record CreateReservation(ParkingSpotId ParkingSpotId, ReservationId ReservationId, Date Date, EmployeeName EmployeeName, LicensePlate LicensePlate);
}
