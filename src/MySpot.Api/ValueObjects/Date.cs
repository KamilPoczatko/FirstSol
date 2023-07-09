﻿namespace MySpot.Api.ValueObjects
{
    public sealed record Date
    {
        public DateTimeOffset Value { get; }

        public Date(DateTimeOffset value)
        {
            Value = value.Date;
        }

        public Date AddDay(int days) => new(Value.AddDays(days));

        public static implicit operator DateTimeOffset(Date date) => date.Value;

        public static implicit operator Date(DateTimeOffset value) => new(value);
        
        //public override string ToString() => Value.ToString();
        //public override int GetHashCode() => Value.GetHashCode();

        public static bool operator <(Date date1, Date date2)
            => date1.Value < date2.Value;
        public static bool operator >(Date date1, Date date2)
            => date1.Value > date2.Value;

        public static bool operator <=(Date date1, Date date2)
            => date1.Value <= date2.Value;
        public static bool operator >=(Date date1, Date date2)
            => date1.Value >= date2.Value;
    }
}
