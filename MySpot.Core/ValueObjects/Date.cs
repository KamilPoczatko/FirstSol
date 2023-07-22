namespace MySpot.Core.ValueObjects
{
    public sealed record Date
    {
        public DateTimeOffset Value { get; }

        public Date(DateTimeOffset value)
        {
            Value = value.Date;
        }
        //public Date(DateTime value)
        //{
        //    Value = value.Date;
        //}

        public Date AddDay(int days) => new(Value.AddDays(days));

        public static implicit operator DateTimeOffset(Date date) => date.Value;

        public static implicit operator Date(DateTimeOffset value) => new(value);


        //public static implicit operator DateTime(Date date) => date.Value.Date;

        //public static implicit operator Date(DateTime value) => new(value.Date);

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
