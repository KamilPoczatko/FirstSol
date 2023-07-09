namespace MySpot.Api.Exceptions
{
    public sealed class ParkingSpotAlreadyReservatedException : CustomException
    {   
        string Name { get; }
        public DateTime Date { get; }

        public ParkingSpotAlreadyReservatedException(string name, DateTime date)
            : base($"Parking spot: {name} is already reserverd at: {date:d}.")
        {
            Name = name;
            Date = date;
        }
    }
}
