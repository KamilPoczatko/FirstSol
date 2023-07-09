﻿using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects
{
    public sealed record ParkingSpotName(string Value)
    {
        public string Value { get; } = Value ?? throw new InvalidParkingSpotNameException();

        public static implicit operator string(ParkingSpotName value) => value.Value;

        public static implicit operator ParkingSpotName(string Value) => new(Value);
    }
}
