using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects
{
    public sealed record ParkingSpotId
    {
        public Guid? Value { get; }

        public ParkingSpotId(Guid? value)
        {
            if (value is null)
            {
                throw new InvalidEnitityIdException();
            }
            Value = value;
        }

        public static implicit operator Guid?(ParkingSpotId value) => value.Value;

        public static implicit operator ParkingSpotId(Guid? Value) => new(Value);

        public static implicit operator string(ParkingSpotId value) => value.Value.ToString();

        public static implicit operator ParkingSpotId(string Value) => new(Value);
    }
}

