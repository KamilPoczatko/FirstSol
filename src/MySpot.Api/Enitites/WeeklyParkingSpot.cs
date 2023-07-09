namespace MySpot.Api.Enitites
{
    public class WeeklyParkingSpot
    {

        private readonly HashSet<Reservation> _reservations = new();

        public Guid Id { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public string Name { get; }
        public IEnumerable<Reservation> reservations => _reservations;
    }
}
