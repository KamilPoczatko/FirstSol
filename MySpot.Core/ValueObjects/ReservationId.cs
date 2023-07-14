using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects
{
    public sealed record ReservationId
    {
        public Guid? Value { get; } 
            
        public ReservationId(Guid? value)
        {
            if(value is null)
            {
                throw new InvalidEnitityIdException();
            }
            Value = value;
        }

        public static implicit operator Guid?(ReservationId value) => value.Value;

        public static implicit operator ReservationId(Guid? Value) => new(Value);

        public static implicit operator string(ReservationId value) => value.Value.ToString();

        public static implicit operator ReservationId(string Value) => new(Value);
    }
}
