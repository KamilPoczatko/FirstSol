using MySpot.Api.ValueObjects;

namespace MySpot.Api.DTO
{
    public class ReservationDto
    {
        public ReservationId Id { get; set; }
        public ParkingSpotId ParkingSpotId { get; set; }
        public EmployeeName EmployeeName { get; set; }
        public Date Date { get; set; }
    }
}
