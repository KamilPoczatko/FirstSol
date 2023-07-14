using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Enitites;

public class Reservation
{
    public ReservationId Id { get; }
    public ParkingSpotId ParkingSpotId { get; private set; }
    public EmployeeName EmployeeName { get; private set; }
    public LicensePlate LicensePlate { get; private set; }
    public Date Date { get; private set; }

    public Reservation(ReservationId id, ParkingSpotId parkingSpotName, EmployeeName employeeName, LicensePlate licensePlate, Date date)
    {
        Id = id;
        ParkingSpotId = parkingSpotName;
        EmployeeName = employeeName;
        ChangeLicensePlate(licensePlate);
        Date = date;
    }

    public void ChangeLicensePlate(LicensePlate licensePlate)
        => LicensePlate = licensePlate;

}
