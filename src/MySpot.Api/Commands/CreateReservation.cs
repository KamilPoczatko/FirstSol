using MySpot.Api.ValueObjects;

namespace MySpot.Api.Commands
{
    public record CreateReservation(ParkingSpotId ParkingSpotId, ReservationId ReservationId, Date Date, EmployeeName EmployeeName, LicensePlate LicensePlate);
}
